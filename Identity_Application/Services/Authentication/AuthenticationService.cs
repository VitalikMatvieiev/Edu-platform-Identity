using Identity_Application.Interfaces;
using Identity_Application.Models;

namespace Identity_Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Register(string username, string email, string password)
    { 
        return new AuthenticationResult(1, username, email, "Token", DateTime.Now);
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(1, "username", email, "Token", DateTime.Now);
    }
}