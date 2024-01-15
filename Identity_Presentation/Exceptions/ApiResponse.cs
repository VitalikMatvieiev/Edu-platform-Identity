namespace Identity_Domain.Exceptions;

public class ApiResponse
{
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "OK",
            400 => "Bad Request",
            401 => "Not Authorized",
            404 => "Not Found",
            500 => "Server Error",
            _ => null
        };
    }
}