namespace Identity_Application.Interfaces;

public interface ILoginRequest
{
    string Email { get; }

    string Password { get; }
}