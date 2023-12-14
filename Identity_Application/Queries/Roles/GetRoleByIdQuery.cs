using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Roles;

public record GetRoleByIdQuery(int Id) : IRequest<Role>;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, Role>
{
    private readonly IGenericRepository<Role> _roleRepository;

    public GetRoleByIdHandler(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _roleRepository
            .GetAsync(r => r.Id == request.Id);

        return result.FirstOrDefault();
    }
}