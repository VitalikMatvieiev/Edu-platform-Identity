using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Identity_Infrastructure.Repositories;

public class ClaimRepository : IClaimRepository
{
    private readonly IdentityDbContext _context;

    public ClaimRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Claim> CreateClaim(Claim toCreate)
    {
        _context.Add(toCreate);
        await _context.SaveChangesAsync();

        return toCreate;
    }

    public async Task DeleteClaim(int claimId)
    {
        var claim = _context.Claim
            .FirstOrDefault(i => i.Id == claimId);

        if (claim is null) return;

        _context.Claim.Remove(claim);

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Claim>> GetAllClaims()
    {
        return await _context.Claim.ToListAsync();
    }

    public async Task<Claim> GetClaimById(int claimId)
    {
        return await _context.Claim.FirstOrDefaultAsync(i => i.Id == claimId);
    }

    public async Task<Claim> UpdateClaim(int claimId, string name)
    {
        var claim = await _context.Claim
            .FirstOrDefaultAsync(i => i.Id == claimId);

        claim.Name = name;

        await _context.SaveChangesAsync();

        return claim;
    }
}