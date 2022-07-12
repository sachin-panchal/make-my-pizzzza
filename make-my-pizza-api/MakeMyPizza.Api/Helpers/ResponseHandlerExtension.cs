using MakeMyPizza.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakeMyPizza.Api.Helpers;

public static class ResponseHandlerExtension
{
    public static IActionResult HandleResponse(this ControllerBase controller, object data)
    {
        var clientResponse = new ClientResponse
        {
            data = data,
            success = true,
            errorList = null
        };
        return controller.Ok(clientResponse);
    }
}
