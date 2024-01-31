using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Identities;

public record DeleteIdentityCommand(int Id) : IRequest;

public class DeleteIdentityHandler : IRequestHandler<DeleteIdentityCommand>
{
    private readonly IGenericRepository<Identity> _identityRepository;

    public DeleteIdentityHandler(IGenericRepository<Identity> identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task Handle(DeleteIdentityCommand request, CancellationToken cancellationToken)
    {
        await _identityRepository.DeleteAsync(request.Id);
    }
}