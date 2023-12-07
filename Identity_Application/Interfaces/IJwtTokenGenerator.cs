namespace Identity_Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(int identityId, string nickname);
}