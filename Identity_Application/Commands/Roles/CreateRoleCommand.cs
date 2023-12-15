using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Additional;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Roles;

public record CreateRoleCommand(RoleVM roleVM) : IRequest<Role>;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Role>
{
    private readonly IGenericRepository<Role> _roleRepository;

    public CreateRoleHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role()
        {
            Name = request.roleVM.Name
        };

        foreach (var claim in request.roleVM.ClaimsIds)
            role.ClaimRole.Add(new ClaimRole()
            {
                Roles = role,
                ClaimsId = claim
            });

        var Role = await _roleRepository
           .InsertAsync(role);

        return Role;
    }
}