namespace Identity_Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<string> Register(string username, string email, string password);

    Task<string> LoginByEmail(string email, string password);

    Task<string> LoginByUsername(string username, string password);
}