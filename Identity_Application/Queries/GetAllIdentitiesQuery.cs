using Identity_Application.Interfaces;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Queries;

public record GetAllIdentitiesQuery : IRequest<List<Identity>>;

public class GetAllIdentitiesHandler : IRequestHandler<GetAllIdentitiesQuery, List<Identity>>
{
    private readonly IIdentityRepository _identityRepository;

    public GetAllIdentitiesHandler(IIdentityRepository identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<List<Identity>> Handle(GetAllIdentitiesQuery request, CancellationToken cancellationToken)
    {
        var result = await _identityRepository.GetAll();
        return result.ToList();
    }
}