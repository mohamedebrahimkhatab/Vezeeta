namespace Vezeeta.Services.Utilities;

public class ServiceResponse
{
    public int StatusCode { get; set; }
    public object? Body { get; set; } = null!;
    public ServiceResponse(int statusCode, object? body)
    {
        StatusCode = statusCode;
        Body = body;
    }
}
