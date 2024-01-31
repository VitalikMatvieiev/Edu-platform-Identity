using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.AppSettingsModels;
using Identity_Domain.Entities.Base;
using Microsoft.Extensions.Options;

namespace Identity_Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private const string includeProps = "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims";
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IGenericRepository<Identity> _identityRepository;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IOptions<PasswordHashSettings> _config;

    public AuthenticationService(IJwtGenerator jwtGenerator,
                                IGenericRepository<Identity> identityRepository,
                                IPasswordHasherService passwordHasher,
                                IOptions<PasswordHashSettings> config)
    {
        _jwtGenerator = jwtGenerator;
        _identityRepository = identityRepository;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    public async Task<string> Register(string username, string email, string password)
    {
        await ValidateUsername(username);
        await ValidateEmail(email);

        var identity = await CreateIdentity(username, email, password);
        var token = _jwtGenerator.GenerateToken(identity);

        return token;
    }

    private async Task ValidateUsername(string username)
    {
        var usernameExists = await _identityRepository
            .GetAsync(i => i.Username == username, includeProperties: includeProps);

        if (usernameExists.Any())
        {
            throw new ArgumentNullException($"Failed to register: User with given username: {username} already exist");
        }
    }

    private async Task ValidateEmail(string email)
    {
        var emailExists = await _identityRepository
            .GetAsync(i => i.Email == email);

        if (emailExists.Any())
        {
            throw new ArgumentNullException($"Failed to register: User with given email: {email} already exist");
        }
    }

    private async Task<Identity> CreateIdentity(string username, string email, string password)
    {
        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher.ComputeHash(password, salt,
                _config.Value.PasswordHashPepper, _config.Value.Iteration);

        var id = await _identityRepository.InsertAsync(
            new Identity
            {
                Username = username,
                Email = email,
                PasswordSalt = salt,
                PasswordHash = hash,
                RegistrationDate = DateTime.UtcNow
            });

        Identity identity;

        try
        {
            var identities = await _identityRepository
            .GetAsync(i => i.Id == id,
            includeProperties: includeProps);

            identity = identities.First();
        }
        catch (ArgumentNullException ex)
        {
            throw new ArgumentNullException("Identity not exist during registration process", ex);
        }

        return identity;
    }

    public async Task<string> LoginByEmail(string email, string password)
    {
        var identity = await RetrieveIdentityByEmail(email);

        ValidatePassword(identity, password);

        await UpdateLastLoginDate(identity);

        return GenerateTokenForIdentity(identity);
    }

    private async Task<Identity> RetrieveIdentityByEmail(string email)
    {
        var identities = await _identityRepository
            .GetAsync(i => i.Email == email, 
                             includeProperties: includeProps);

        var identity = identities.FirstOrDefault();

        if (identity is null)
        {
            throw new UnauthorizedAccessException($"Failed to login: Incorrect email: {email}");
        }

        return identity;
    }

    public async Task<string> LoginByUsername(string username, string password)
    {
        var identity = await RetrieveIdentityByUsername(username);

        ValidatePassword(identity, password);

        await UpdateLastLoginDate(identity);

        return GenerateTokenForIdentity(identity);
    }

    private async Task<Identity> RetrieveIdentityByUsername(string username)
    {
        var identities = await _identityRepository
            .GetAsync(i => i.Username == username,
                             includeProperties: includeProps);

        var identity = identities.FirstOrDefault();

        if (identity is null)
        {
            throw new UnauthorizedAccessException($"Failed to login: Incorrect username: {username}");
        }

        return identity;
    }

    private void ValidatePassword(Identity identity, string password)
    {
        var passwordHash = _passwordHasher
            .ComputeHash(password, identity.PasswordSalt,
                _config.Value.PasswordHashPepper, _config.Value.Iteration);

        if (identity.PasswordHash != passwordHash)
        {
            throw new UnauthorizedAccessException($"Failed to login: Incorrect password: {password}");
        }
    }

    private async Task UpdateLastLoginDate(Identity identity)
    {
        identity.LastLogin = DateTime.UtcNow;
        await _identityRepository.UpdateAsync(identity);
    }

    private string GenerateTokenForIdentity(Identity identity)
    {
        return _jwtGenerator.GenerateToken(identity);
    }
}