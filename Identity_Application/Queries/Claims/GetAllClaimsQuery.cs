using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Claims;

public record GetAllClaimsQuery : IRequest<List<Claim>>;

public class GetAllClaimsHandler : IRequestHandler<GetAllClaimsQuery, List<Claim>>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public GetAllClaimsHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<List<Claim>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
    {
        var result = await _claimRepository
            .GetAsync();

        return result.ToList();
    }
}