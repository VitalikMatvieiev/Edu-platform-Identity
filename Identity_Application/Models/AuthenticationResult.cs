namespace Identity_Application.Models;

public record AuthenticationResult(int Id, string Username, string Email, string Token, DateTime TokenExpiration);