using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record UpdateClaimCommand(int Id, ClaimVM ClaimVM) : IRequest;

public class UpdateClaimHandler : IRequestHandler<UpdateClaimCommand>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public UpdateClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
    {
        var claims = await _claimRepository.GetAsync(c => c.Id == request.Id);
        var claim = claims.FirstOrDefault();

        if (claim is null)
            throw new Exception("Claim not found.");

        claim.Name = request.ClaimVM.Name;

        await _claimRepository.UpdateAsync(claim);
    }
}