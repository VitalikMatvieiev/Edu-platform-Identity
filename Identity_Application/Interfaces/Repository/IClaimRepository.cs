using Identity_Domain.Entities.Base;

namespace Identity_Application.Interfaces.Repository;

public interface IClaimRepository
{
    Task<ICollection<Claim>> GetAllClaims();

    Task<Claim> CreateClaim(Claim toCreate);

    Task<Claim> GetClaimById(int claimId);

    Task<Claim> UpdateClaim(int claimId, string name);

    Task DeleteClaim(int claimId);
}