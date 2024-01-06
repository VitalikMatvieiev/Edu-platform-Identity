using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record CreateClaimCommand(ClaimVM vm) : IRequest<int>;

public class CreateClaimHandler : IRequestHandler<CreateClaimCommand, int>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public CreateClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<int> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = new Claim()
        {
            Name = request.vm.Name
        };

        var id = await _claimRepository.InsertAsync(claim);

        return id;
    }
}