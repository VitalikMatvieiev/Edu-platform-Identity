namespace Identity_Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<string> Register(string username, string email, string password);

    Task<string> Login(string email, string password);
}