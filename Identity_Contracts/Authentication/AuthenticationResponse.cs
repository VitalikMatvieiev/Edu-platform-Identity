namespace Identity_Contracts.Entities.Authentication;

public record AuthenticationResponse(int Id, string Username, string Email, string Token, DateTime TokenExpiration);