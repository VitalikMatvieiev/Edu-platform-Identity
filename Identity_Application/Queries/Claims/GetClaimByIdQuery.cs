using AutoMapper;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Claims;

public record GetClaimByIdQuery(int Id) : IRequest<ClaimDTO>;

public class GetClaimByIdHandler : IRequestHandler<GetClaimByIdQuery, ClaimDTO>
{
    private readonly IGenericRepository<Claim> _claimRepository;
    private readonly IMapper _mapper;

    public GetClaimByIdHandler(IGenericRepository<Claim> claimRepository, IMapper mapper)
    {
        _claimRepository = claimRepository;
        _mapper = mapper;
    }

    public async Task<ClaimDTO> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var claims = await _claimRepository
            .GetAsync(c => c.Id == request.Id);

        var claim = claims.FirstOrDefault();

        if (claim is null)
            throw new Exception("Claim with given id was not found");

        var result = _mapper
            .Map<ClaimDTO>(claim);

        return result;
    }
}