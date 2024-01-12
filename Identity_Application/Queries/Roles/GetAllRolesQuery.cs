using AutoMapper;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Roles;

public record GetAllRolesQuery : IRequest<List<RoleDTO>>;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<RoleDTO>>
{
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IMapper _mapper;

    public GetAllRolesHandler(IGenericRepository<Role> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<List<RoleDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository
            .GetAsync(includeProperties: "ClaimRole,ClaimRole.Claims");

        var result = roles.Select(r =>
            _mapper.Map<RoleDTO>(r));

        return result.ToList();
    }
}