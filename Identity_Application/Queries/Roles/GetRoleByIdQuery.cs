using AutoMapper;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Roles;

public record GetRoleByIdQuery(int Id) : IRequest<RoleDTO>;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleDTO>
{
    private const string includeProps = "ClaimRole,ClaimRole.Claims";
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IMapper _mapper;

    public GetRoleByIdHandler(IGenericRepository<Role> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<RoleDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository
            .GetAsync(r => r.Id == request.Id, includeProperties: includeProps);

        var role = roles.FirstOrDefault();

        if (role is null)
            throw new Exception("Role with given id was not found");

        var result = _mapper
            .Map<RoleDTO>(role);

        return result;
    }
}