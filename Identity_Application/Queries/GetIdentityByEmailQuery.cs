using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries;

public record GetIdentityByEmailQuery(string email) : IRequest<Identity>;

public class GetIdentityByEmailHandler : IRequestHandler<GetIdentityByEmailQuery, Identity>
{
    private readonly IGenericRepository<Identity> _identityRepository;

    public GetIdentityByEmailHandler(IGenericRepository<Identity> identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<Identity> Handle(GetIdentityByEmailQuery request, CancellationToken cancellationToken)
    {
        var result = await _identityRepository.GetAsync(i => i.Email == request.email);

        return result.FirstOrDefault();
    }
}