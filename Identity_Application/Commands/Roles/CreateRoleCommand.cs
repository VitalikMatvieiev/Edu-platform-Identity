using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Additional;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Roles;

public record CreateRoleCommand(RoleVM RoleVM) : IRequest<int>;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IValidator<RoleVM> _validator;

    public CreateRoleHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
        _validator = new RoleValidator();
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.RoleVM is null)
            throw new ArgumentNullException("Given data is not correct");

        var errors = _validator.Validate(request.RoleVM);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        var role = new Role()
        {
            Name = request.RoleVM.Name
        };

        foreach (var claim in request.RoleVM.ClaimsIds)
            role.ClaimRole.Add(new ClaimRole()
            {
                Roles = role,
                ClaimsId = claim
            });

        var id = await _roleRepository
           .InsertAsync(role);

        return id;
    }
}