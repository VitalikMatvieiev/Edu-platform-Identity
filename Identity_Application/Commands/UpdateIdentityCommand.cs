using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands;

public record UpdateIdentityCommand(Identity identity) : IRequest<Identity>;

public class EditIdentityHandler : IRequestHandler<UpdateIdentityCommand, Identity>
{
    private readonly IIdentityRepository _identityRepository;

    public EditIdentityHandler(IIdentityRepository identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<Identity> Handle(UpdateIdentityCommand request, CancellationToken cancellationToken)
    {
        var identity  = await _identityRepository.UpdateIdentity(request.identity);

        return identity;
    }
}