using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.AppSettingsModels;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity_Application.Commands.Identities;

public record CreateIdentityCommand(IdentityVM IdentityVM) : IRequest<int>;

public class CreateIdentityHandler : IRequestHandler<CreateIdentityCommand, int>
{
    private readonly IGenericRepository<Identity> _identityRepository;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IOptions<PasswordHashSettings> _config;
    private readonly IValidator<IdentityVM> _validator;

    public CreateIdentityHandler(IGenericRepository<Identity> identityRepository,
                                 IPasswordHasherService passwordHasher,
                                 IOptions<PasswordHashSettings> config)
    {
        _identityRepository = identityRepository;
        _passwordHasher = passwordHasher;
        _config = config;
        _validator = new IdentityValidator();
    }

    public async Task<int> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
    {
        if (request.IdentityVM is null)
            throw new ArgumentNullException("Given data is not correct");

        var errors = _validator.Validate(request.IdentityVM);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        if (_config is null)
            throw new SystemException("Error occured during operation");

        var salt = _passwordHasher.GenerateSalt();

        if (String.IsNullOrEmpty(salt))
            throw new Exception("Error occured during password hashing");

        var hash = _passwordHasher
            .ComputeHash(request.IdentityVM.Password, salt,
                        _config.Value.PasswordHashPepper, _config.Value.Iteration);

        if (String.IsNullOrEmpty(hash))
            throw new Exception("Error occured during password hashing");

        var identity = new Identity
        {
            Username = request.IdentityVM.Username,
            Email = request.IdentityVM.Email,
            PasswordSalt = salt,
            PasswordHash = hash,
            RegistrationDate = DateTime.UtcNow
        };

        foreach (var claim in request.IdentityVM.ClaimsIds)
            identity.ClaimIdentities.Add(new Identity_Domain.Entities.Additional.ClaimIdentity
            {
                Identities = identity,
                ClaimsId = claim
            });

        foreach (var role in request.IdentityVM.RolesIds)
            identity.IdentityRole.Add(new Identity_Domain.Entities.Additional.IdentityRole
            {
                Identities = identity,
                RolesId = role
            });

        var id = await _identityRepository.InsertAsync(identity);

        return id;
    }
}