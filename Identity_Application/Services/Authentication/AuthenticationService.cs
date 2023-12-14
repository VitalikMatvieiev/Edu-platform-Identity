using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.AppSettingsModels;
using Identity_Domain.Entities.Base;
using Microsoft.Extensions.Options;

namespace Identity_Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
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
        var errors = new List<string>();

        //Check if username is not in db
        var usernameIdentities = await _identityRepository
            .GetAsync(i => i.Username == username);

        var usernameIdentity = usernameIdentities.FirstOrDefault();

        if (usernameIdentity is not null)
            errors.Add("User with given username already exist");

        //Check if email is not in db
        var emailIdentities = await _identityRepository
            .GetAsync(i => i.Email == email);

        var emailIdentity = emailIdentities.FirstOrDefault();

        if (emailIdentity is not null)
            errors.Add("User with given email already exist");

        //Throw all exceptions
        if (errors.Count > 0)
            foreach (var error in errors)
                throw new Exception($"Failed to register: {error}");

        //If all is good

        //write in data
        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher.ComputeHash(password, salt,
                _config.Value.PasswordHashPepper, _config.Value.Iteration);

        var identity = await _identityRepository.InsertAsync(
            new Identity
            {
                Username = username,
                Email = email,
                PasswordSalt = salt,
                PasswordHash = hash,
                RegistrationDate = DateTime.UtcNow
            });

        //Generate token
        var token = _jwtGenerator.GenerateToken(identity);

        return token;
    }

    public async Task<string> Login(string email, string password)
    {
        var errors = new List<string>();

        var identities = await _identityRepository
            .GetAsync(i => i.Email == email,
            includeProperties: "Claims,Roles,Roles.Claims");

        var identity = identities.FirstOrDefault();

        //Check for the email is correct
        if (identity is null)
            throw new Exception($"Failed to login: \"Incorrect email\"");

        //Check if password is correct
        var passwordHash = _passwordHasher
            .ComputeHash(password, identity.PasswordSalt, 
                _config.Value.PasswordHashPepper, _config.Value.Iteration);

        if (identity.PasswordHash != passwordHash)
            throw new Exception($"Failed to login: \"Incorrect password\"");

        //If all is good

        //Change Last login date
        identity.LastLogin = DateTime.UtcNow;

        await _identityRepository.UpdateAsync(identity);

        //Create token
        var token = _jwtGenerator.GenerateToken(identity);

        return token;
    }
}