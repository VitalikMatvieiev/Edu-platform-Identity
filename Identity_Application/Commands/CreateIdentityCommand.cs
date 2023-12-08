using Identity_Application.Interfaces;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands;

public record CreateIdentityCommand(string Username, string Email, string Password) : IRequest<Identity>;

public class CreateIdentityHandler : IRequestHandler<CreateIdentityCommand, Identity>
{
    private readonly IIdentityRepository _identityRepository;

    public CreateIdentityHandler(IIdentityRepository identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<Identity> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
    {
        await _identityRepository.AddIdentity(new Identity { Username = request.Username, Email = request.Email, Password = request.Password });

        var identity = await _identityRepository.GetIdentityByEmail(request.Email);

        return identity;
    }
}