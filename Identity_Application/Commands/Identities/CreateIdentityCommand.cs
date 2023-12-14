using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.AppSettingsModels;
using Identity_Domain.Entities.Base;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity_Application.Commands.Identities;

public record CreateIdentityCommand(string Username, string Email, string Password) : IRequest<Identity>;

public class CreateIdentityHandler : IRequestHandler<CreateIdentityCommand, Identity>
{
    private readonly IGenericRepository<Identity> _identityRepository;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IOptions<PasswordHashSettings> _config;

    public CreateIdentityHandler(IGenericRepository<Identity> identityRepository, 
                                 IPasswordHasherService passwordHasher,
                                 IOptions<PasswordHashSettings> config)
    {
        _identityRepository = identityRepository;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    public async Task<Identity> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
    {
        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher
            .ComputeHash(request.Password, salt, 
                        _config.Value.PasswordHashPepper, _config.Value.Iteration);

        var identity = await _identityRepository.InsertAsync(
            new Identity
            {
                Username = request.Username,
                Email = request.Email,
                PasswordSalt = salt,
                PasswordHash = hash,
                RegistrationDate = DateTime.UtcNow
            });

        return identity;
    }
}