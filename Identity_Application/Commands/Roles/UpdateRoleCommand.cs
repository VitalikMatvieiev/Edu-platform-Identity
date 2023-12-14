using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Roles;

public record UpdateRoleCommand(Role Role) : IRequest;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IGenericRepository<Role> _roleRepository;

    public UpdateRoleHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleRepository.UpdateAsync(request.Role);
    }
}