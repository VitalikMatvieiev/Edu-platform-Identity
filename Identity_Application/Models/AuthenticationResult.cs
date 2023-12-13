namespace Identity_Application.Models;

public record AuthenticationResult(bool IsSuccess, List<string> Errors);