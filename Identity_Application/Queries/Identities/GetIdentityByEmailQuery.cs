using AutoMapper;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Identities;

public record GetIdentityByEmailQuery(string Email) : IRequest<IdentityDTO>;

public class GetIdentityByEmailHandler : IRequestHandler<GetIdentityByEmailQuery, IdentityDTO>
{
    private const string includeProps = "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims";
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
            .GetAsync(i => i.Email == request.Email, 
            includeProperties: includeProps);

        var identity = identities.FirstOrDefault();

        if (identity is null)
            throw new Exception("Identity with given email was not found");

        var result = _mapper
            .Map<IdentityDTO>(identity);

        return result;
    }
}