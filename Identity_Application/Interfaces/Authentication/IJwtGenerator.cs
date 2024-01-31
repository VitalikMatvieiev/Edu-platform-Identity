using Identity_Domain.Entities.Base;

namespace Identity_Application.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(Identity identity);
}