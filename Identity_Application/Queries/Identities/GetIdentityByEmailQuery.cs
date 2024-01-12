using AutoMapper;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Identities;

public record GetIdentityByEmailQuery(string email) : IRequest<IdentityDTO>;

public class GetIdentityByEmailHandler : IRequestHandler<GetIdentityByEmailQuery, IdentityDTO>
{
    private readonly IGenericRepository<Identity> _identityRepository;
    private readonly IMapper _mapper;

    public GetIdentityByEmailHandler(IGenericRepository<Identity> identityRepository, IMapper mapper)
    {
        _identityRepository = identityRepository;
        _mapper = mapper;
    }

    public async Task<IdentityDTO> Handle(GetIdentityByEmailQuery request, CancellationToken cancellationToken)
    {
        var identities = await _identityRepository
            .GetAsync(i => i.Email == request.email, 
            includeProperties: "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims");

        var identity = identities.FirstOrDefault();

        var result = _mapper
            .Map<IdentityDTO>(identity);

        return result;
    }
}