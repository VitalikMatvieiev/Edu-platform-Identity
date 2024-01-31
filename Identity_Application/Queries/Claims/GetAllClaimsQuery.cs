using AutoMapper;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Claims;

public record GetAllClaimsQuery : IRequest<List<ClaimDTO>>;

public class GetAllClaimsHandler : IRequestHandler<GetAllClaimsQuery, List<ClaimDTO>>
{
    private readonly IGenericRepository<Claim> _claimRepository;
    private readonly IMapper _mapper;

    public GetAllClaimsHandler(IGenericRepository<Claim> claimRepository, IMapper mapper)
    {
        _claimRepository = claimRepository;
        _mapper = mapper;
    }

    public async Task<List<ClaimDTO>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
    {
        var claims = await _claimRepository
            .GetAsync();

        var result = claims.Select(c => 
            _mapper.Map<ClaimDTO>(c));

        return result.ToList();
    }
}