using System.Net;
using System.Security.Authentication;
using Newtonsoft.Json;

using MakeMyPizza.Data.DataExceptions;

namespace MakeMyPizza.Api.Middlewares;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next,
                                ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case NullReferenceException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case AuthenticationException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case RecordNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case RecordAlreadyExistsException e:
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            _logger.LogError(error, error.Message);
            var result = JsonConvert.SerializeObject(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}

public static class ErrorHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseErrorHandlerMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}