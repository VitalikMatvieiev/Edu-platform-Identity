namespace Identity_Application.Interfaces;

public interface IAuthenticationResponse
{
    public int Id { get; }

    public string Userame { get; }

    public string Email { get; }

    public string Token { get; }

    public DateTime TokenExpiration { get; }
}