using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record CreateClaimCommand(ClaimVM vm) : IRequest<Claim>;

public class CreateClaimHandler : IRequestHandler<CreateClaimCommand, Claim>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public CreateClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<Claim> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = new Claim()
        {
            Name = request.vm.Name
        };

        var Claim = await _claimRepository.InsertAsync(claim);

        return Claim;
    }
}