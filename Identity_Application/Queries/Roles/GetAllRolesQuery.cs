using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Roles;

public record GetAllRolesQuery : IRequest<List<Role>>;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<Role>>
{
    private readonly IGenericRepository<Role> _roleRepository;

    public GetAllRolesHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var result = await _roleRepository
            .GetAsync();

        return result.ToList();
    }
}