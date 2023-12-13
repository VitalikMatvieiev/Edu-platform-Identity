using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models;

namespace Identity_Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(string username, string email, string password)
    {
        var errors = new List<string>();

        //Check if username is not in db

        //Check if email is not in db

        //write in password

        return new AuthenticationResult(true, errors);
    }

    public AuthenticationResult Login(string email, string password)
    {
        var errors = new List<string>();

        //Check for the email is correct

        //Check if password is correct

        return new AuthenticationResult(true, errors);
    }
}