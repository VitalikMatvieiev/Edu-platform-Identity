using Identity_Application.Interfaces;
using Identity_Application.Models;

namespace Identity_Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    //private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(/*IJwtTokenGenerator jwtTokenGenerator*/)
    {
        //_jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(string username, string email, string password)
    {
        var identityId = 2;

        //var token = _jwtTokenGenerator.GenerateToken(identityId, username);

        return new AuthenticationResult(1, username, email, "Token", DateTime.Now);
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(1, "username", email, "Token", DateTime.Now);
    }
}