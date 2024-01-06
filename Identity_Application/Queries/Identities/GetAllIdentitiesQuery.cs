using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries.Identities;

public record GetAllIdentitiesQuery : IRequest<List<Identity>>;

public class GetAllIdentitiesHandler : IRequestHandler<GetAllIdentitiesQuery, List<Identity>>
{
    private readonly IGenericRepository<Identity> _identityRepository;

    public GetAllIdentitiesHandler(IGenericRepository<Identity> identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<List<Identity>> Handle(GetAllIdentitiesQuery request, CancellationToken cancellationToken)
    {
        var result = await _identityRepository
            .GetAsync(includeProperties: "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims");

        return result.ToList();
    }
}