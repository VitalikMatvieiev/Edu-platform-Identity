using AutoMapper;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Identities;

public record GetAllIdentitiesQuery : IRequest<List<IdentityDTO>>;

public class GetAllIdentitiesHandler : IRequestHandler<GetAllIdentitiesQuery, List<IdentityDTO>>
{
    private const string includeProps = "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims";
    private readonly IGenericRepository<Identity> _identityRepository;
    private readonly IMapper _mapper;

    public GetAllIdentitiesHandler(IGenericRepository<Identity> identityRepository, IMapper mapper)
    {
        _identityRepository = identityRepository;
        _mapper = mapper;
    }

    public async Task<List<IdentityDTO>> Handle(GetAllIdentitiesQuery request, CancellationToken cancellationToken)
    {
        var identities = await _identityRepository
            .GetAsync(includeProperties: includeProps);

        var result = identities.Select(i =>
            _mapper.Map<IdentityDTO>(i));

        return result.ToList();
    }
}