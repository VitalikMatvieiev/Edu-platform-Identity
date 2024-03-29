﻿using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.AppSettingsModels;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;
using Microsoft.Extensions.Options;
using System.Data;

namespace Identity_Application.Commands.Identities;

public record UpdateIdentityCommand(int Id, IdentityVM IdentityVM) : IRequest;

public class EditIdentityHandler : IRequestHandler<UpdateIdentityCommand>
{
    private readonly IGenericRepository<Identity> _identityRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IOptions<PasswordHashSettings> _config;
    private readonly IValidator<IdentityVM> _validator;

    public EditIdentityHandler(IGenericRepository<Identity> identityRepository,
                               IPasswordHasherService passwordHasherService,
                               IOptions<PasswordHashSettings> config)
    {
        _identityRepository = identityRepository;
        _passwordHasherService = passwordHasherService;
        _config = config;
        _validator = new IdentityValidator();
    }

    public async Task Handle(UpdateIdentityCommand request, CancellationToken cancellationToken)
    {
        if (request.IdentityVM is null)
            throw new ArgumentNullException("Given data is not correct");

        var errors = _validator.Validate(request.IdentityVM);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        var identities = await _identityRepository
            .GetAsync(i => i.Id == request.Id, 
            includeProperties: "ClaimIdentities.Claims,IdentityRole.Roles");

        var identity = identities.FirstOrDefault();

        if (identity is null)
            throw new Exception("Identity with given id was not found.");

        var salt = _passwordHasherService.GenerateSalt();
        var hash = _passwordHasherService.ComputeHash(request.IdentityVM.Password, salt,
                                    _config.Value.PasswordHashPepper, _config.Value.Iteration);

        identity.Username = request.IdentityVM.Username;
        identity.Email = request.IdentityVM.Email;
        identity.PasswordSalt = salt;
        identity.PasswordHash = hash;

        var existingClaimIds = identity.ClaimIdentities.Select(x => x.ClaimsId).ToList();
        var selectedClaimIds = request.IdentityVM.ClaimsIds.ToList();
        var toAddClaims = selectedClaimIds.Except(existingClaimIds);
        var toRemoveClaims = existingClaimIds.Except(selectedClaimIds);

        identity.ClaimIdentities = identity.ClaimIdentities.Where(x => !toRemoveClaims.Contains(x.ClaimsId)).ToList();

        foreach (var item in toAddClaims)
        {
            identity.ClaimIdentities.Add(new Identity_Domain.Entities.Additional.ClaimIdentity
            {
                ClaimsId = item
            });
        }

        var existingRoleIds = identity.IdentityRole.Select(x => x.RolesId).ToList();
        var selectedRoleIds = request.IdentityVM.RolesIds.ToList();
        var toAddRoles = selectedRoleIds.Except(existingRoleIds);
        var toRemoveRoles = existingRoleIds.Except(selectedRoleIds);

        identity.IdentityRole = identity.IdentityRole.Where(x => !toRemoveRoles.Contains(x.RolesId)).ToList();

        foreach (var item in toAddRoles)
        {
            identity.IdentityRole.Add(new Identity_Domain.Entities.Additional.IdentityRole
            {
                RolesId = item
            });
        }

        await _identityRepository.UpdateAsync(identity);
    }
}