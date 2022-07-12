
namespace MakeMyPizza.Api.Models;

public class ClientResponse
{
    public object? data { get; set; }
    public bool success { get; set; }
    public List<ErrorDetails>? errorList { get; set; }
}

public class ErrorDetails
{
    public int statusCode { get; set; }
    public string? message { get; set; }
}
