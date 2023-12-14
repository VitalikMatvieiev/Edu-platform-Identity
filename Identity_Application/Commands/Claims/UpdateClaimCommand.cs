using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record UpdateClaimCommand(Claim Claim) : IRequest;

public class UpdateClaimHandler : IRequestHandler<UpdateClaimCommand>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public UpdateClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
    {
        await _claimRepository.UpdateAsync(request.Claim);
    }
}