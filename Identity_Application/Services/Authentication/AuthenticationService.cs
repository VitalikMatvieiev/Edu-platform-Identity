using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace Identity_Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IGenericRepository<Identity> _identityRepository;

    public AuthenticationService(IJwtGenerator jwtGenerator,
                                IGenericRepository<Identity> identityRepository)
    {
        _jwtGenerator = jwtGenerator;
        _identityRepository = identityRepository;
    }

    public async Task<string> Register(string username, string email, string password)
    {
        var errors = new List<string>();

        //Check if username is not in db
        var usernameCheck = (await _identityRepository
            .GetAsync(i => i.Username == username)).First();

        if (usernameCheck is not null)
            errors.Add("User with given username already exist");

        //Check if email is not in db
        var emailCheck = (await _identityRepository
            .GetAsync(i => i.Email == email)).First();

        if (emailCheck is not null)
            errors.Add("User with given email already exist");

        //Throw all exceptions
        if (errors.Count > 0)
            foreach (var error in errors)
                throw new Exception($"Failed to register: {error}");

        //If all is good

        //write in data
        var identity = await _identityRepository.InsertAsync(
            new Identity
            {
                Username = username,
                Email = email,
                Password = password,
                RegistrationDate = DateTime.UtcNow
            });

        //Generate token
        var token = _jwtGenerator.GenerateToken(identity);

        return token;
    }

    public async Task<string> Login(string email, string password)
    {
        var errors = new List<string>();

        var identity = (await _identityRepository
            .GetAsync(i => i.Email == email,
            includeProperties: "Claims,Roles,Roles.Claims")).First();

        //Check for the email is correct
        if (identity is null)
            throw new Exception($"Failed to login: \"Incorrect email\"");

        //Check if password is correct
        if (identity.Password != password)
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