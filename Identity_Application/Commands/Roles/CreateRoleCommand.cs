using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Roles;

public record CreateRoleCommand(Role Role) : IRequest<Role>;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Role>
{
    private readonly IGenericRepository<Role> _roleRepository;

    public CreateRoleHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository
            .InsertAsync(request.Role);

        return role;
    }
}