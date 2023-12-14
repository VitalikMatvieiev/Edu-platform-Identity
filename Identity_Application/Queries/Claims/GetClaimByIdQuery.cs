using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Claims;

public record GetClaimByIdQuery(int Id) : IRequest<Claim>;

public class GetClaimByIdHandler : IRequestHandler<GetClaimByIdQuery, Claim>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public GetClaimByIdHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<Claim> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _claimRepository
            .GetAsync(c => c.Id == request.Id);

        return result.FirstOrDefault();
    }
}