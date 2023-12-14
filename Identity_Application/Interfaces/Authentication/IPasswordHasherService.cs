namespace Identity_Application.Interfaces.Authentication;

public interface IPasswordHasherService
{
    string ComputeHash(string password, string salt, string pepper, int iteration);

    string GenerateSalt();
}