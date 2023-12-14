using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Identities;

public record CreateIdentityCommand(string Username, string Email, string Password) : IRequest<Identity>;

public class CreateIdentityHandler : IRequestHandler<CreateIdentityCommand, Identity>
{
    private readonly IGenericRepository<Identity> _identityRepository;

    public CreateIdentityHandler(IGenericRepository<Identity> identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<Identity> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
    {
        var identity = await _identityRepository.InsertAsync(
            new Identity
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                RegistrationDate = DateTime.UtcNow
            });

        return identity;
    }
}