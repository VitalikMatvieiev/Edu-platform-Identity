namespace Identity_Contracts.Entities.Authentication;

public record RegisterRequest(string Username, string Email, string Password);