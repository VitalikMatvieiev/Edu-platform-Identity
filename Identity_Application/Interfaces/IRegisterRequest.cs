namespace Identity_Application.Interfaces;

public interface IRegisterRequest
{
    string Username { get; }

    string Email { get; }

    string Password { get; }
}