using Identity_Application.Interfaces;
using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Identity_Infrastructure.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly IdentityDbContext _context;

    public IdentityRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Identity> AddIdentity(Identity toCreate)
    {
        _context.Identity.Add(toCreate);
        await _context.SaveChangesAsync();

        return toCreate;
    }

    public async Task DeleteIdentity(int personId)
    {
        var person = _context.Identity
            .FirstOrDefault(i => i.Id == personId);

        if (person is null) return;

        _context.Identity.Remove(person);

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Identity>> GetAll()
    {
        return await _context.Identity.ToListAsync();
    }

    public async Task<Identity> GetIdentityById(int identityId)
    {
        return await _context.Identity.FirstOrDefaultAsync(i => i.Id == identityId);
    }

    public async Task<Identity> UpdateIdentity(int identityId, string username, string email)
    {
        var identity = await _context.Identity
            .FirstOrDefaultAsync(i => i.Id == identityId);

        identity.Username = username;
        identity.Email = email;

        await _context.SaveChangesAsync();

        return identity;
    }
}