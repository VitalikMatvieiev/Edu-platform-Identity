using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.AppSettingsModels;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity_Application.Commands.Identities;

public record CreateIdentityCommand(IdentityVM vm) : IRequest<Identity>;

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
            .ComputeHash(request.vm.Password, salt,
                        _config.Value.PasswordHashPepper, _config.Value.Iteration);

        var identity = new Identity
        {
            Username = request.vm.Username,
            Email = request.vm.Email,
            PasswordSalt = salt,
            PasswordHash = hash,
            RegistrationDate = DateTime.UtcNow
        };

        foreach (var claim in request.vm.ClaimsIds)
            identity.ClaimIdentities.Add(new Identity_Domain.Entities.Additional.ClaimIdentity
            {
                Identities = identity,
                ClaimsId = claim
            });

        foreach (var role in request.vm.RolesIds)
            identity.IdentityRole.Add(new Identity_Domain.Entities.Additional.IdentityRole
            {
                Identities = identity,
                RolesId = role
            });

        var Identity = await _identityRepository.InsertAsync(identity);

        return Identity;
    }
}