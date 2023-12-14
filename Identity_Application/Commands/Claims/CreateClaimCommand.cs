using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record CreateClaimCommand(string Name) : IRequest<Claim>;

public class CreateClaimHandler : IRequestHandler<CreateClaimCommand, Claim>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public CreateClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<Claim> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _claimRepository.InsertAsync(
            new Claim
            {
                Name = request.Name,
            });

        return claim;
    }
}