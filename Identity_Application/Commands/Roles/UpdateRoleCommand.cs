using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Roles;

public record UpdateRoleCommand(int Id, RoleVM vm) : IRequest;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IGenericRepository<Role> _roleRepository;

    public UpdateRoleHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository
            .GetAsync(r => r.Id == request.Id, includeProperties: "ClaimRole.Claims");

        var role = roles.FirstOrDefault();

        role.Name = request.vm.Name;

        var existingIds = role.ClaimRole.Select(x => x.ClaimsId);
        var selectedIds = request.vm.ClaimsIds.ToList();
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