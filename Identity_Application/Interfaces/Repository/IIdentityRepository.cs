using Identity_Domain.Entities.Base;

namespace Identity_Application.Interfaces.Repository;

public interface IIdentityRepository
{
    Task<ICollection<Identity>> GetAllIdentities();

    Task<Identity> GetIdentityById(int identityId);

    Task<Identity> GetIdentityByEmail(string email);

    Task<Identity> AddIdentity(Identity toCreate);

    Task<Identity> UpdateIdentity(Identity identity);

    Task DeleteIdentity(int identityId);
}