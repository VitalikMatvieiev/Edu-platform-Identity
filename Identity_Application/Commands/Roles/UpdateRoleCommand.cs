using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Roles;

public record UpdateRoleCommand(int Id, RoleVM RoleVM) : IRequest;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IValidator<RoleVM> _validator;

    public UpdateRoleHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
        _validator = new RoleValidator();
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.RoleVM is null)
            throw new ArgumentNullException("Given data is not correct");

        var errors = _validator.Validate(request.RoleVM);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        var roles = await _roleRepository
            .GetAsync(r => r.Id == request.Id, includeProperties: "ClaimRole.Claims");

        var role = roles.FirstOrDefault();

        if (role is null)
            throw new Exception("Role with given id was not found.");

        role.Name = request.RoleVM.Name;

        var existingIds = role.ClaimRole.Select(x => x.ClaimsId);
        var selectedIds = request.RoleVM.ClaimsIds.ToList();
        var toAdd = selectedIds.Except(existingIds);
        var toRemove = existingIds.Except(selectedIds);

        role.ClaimRole = role.ClaimRole.Where(x => !toRemove.Contains(x.ClaimsId)).ToList();

        if (toAdd is not null)
            foreach (var item in toAdd)
            {
                role.ClaimRole.Add(new Identity_Domain.Entities.Additional.ClaimRole()
                {
                    ClaimsId = item
                });
            }

        await _roleRepository.UpdateAsync(role);
    }
}