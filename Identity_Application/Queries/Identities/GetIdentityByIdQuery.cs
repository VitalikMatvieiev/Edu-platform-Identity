using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Identities;

public record GetIdentityByIdQuery(int Id) : IRequest<Identity>;

public class GetIdentityByIdHandler : IRequestHandler<GetIdentityByIdQuery, Identity>
{
    private readonly IGenericRepository<Identity> _identityRepository;

    public GetIdentityByIdHandler(IGenericRepository<Identity> identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<Identity> Handle(GetIdentityByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _identityRepository
            .GetAsync(i => i.Id == request.Id,
            includeProperties: "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims");

        return result.FirstOrDefault();
    }
}