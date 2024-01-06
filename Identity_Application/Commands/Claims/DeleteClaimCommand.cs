using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record DeleteClaimCommand(int Id) : IRequest;

public class DeleteClaimHandler : IRequestHandler<DeleteClaimCommand>
{
    private readonly IGenericRepository<Claim> _claimRepository;

    public DeleteClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        await _claimRepository.DeleteAsync(request.Id);
    }
}