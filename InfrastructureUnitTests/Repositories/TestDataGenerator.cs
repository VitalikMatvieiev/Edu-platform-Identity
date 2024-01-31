using Identity_Domain.Entities.Additional;
using Identity_Domain.Entities.Base;

namespace InfrastructureUnitTests.Repositories;

public static class TestDataGenerator
{
    private static readonly Random Random = new Random();

    private static readonly List<string> SampleClaimNames = new List<string>
    {
        "ViewProfile", "EditProfile", "DeleteAccount", "CreatePost", "EditPost", "DeletePost"
    };

    private static readonly List<string> SampleRoleNames = new List<string>
    {
        "User", "Admin", "Moderator", "Guest", "Subscriber", "Editor"
    };

    private static readonly HashSet<int> UsedClaimIds = new HashSet<int>();
    private static readonly HashSet<int> UsedRoleIds = new HashSet<int>();
    private static readonly HashSet<int> UsedIdentityIds = new HashSet<int>();
    private static readonly Random _random = new Random();

    private static int GetUniqueRandomId(HashSet<int> ids)
    {
        int id;
        do
        {
            id = _random.Next(1, 1000);
        } while (!ids.Add(id));
        return id;
    }

    public static Claim GetRandomClaim()
    {
        var name = SampleClaimNames[Random.Next(SampleClaimNames.Count)];
        return new Claim { Id = GetUniqueRandomId(UsedClaimIds), Name = name };
    }

    public static Role GetRandomRole()
    {
        var name = SampleRoleNames[Random.Next(SampleRoleNames.Count)];
        var role = new Role { Id = GetUniqueRandomId(UsedRoleIds), Name = name, ClaimRole = new List<ClaimRole>() };

        int numberOfClaims = Random.Next(4);
        for (int i = 0; i < numberOfClaims; i++)
        {
            var claim = GetRandomClaim();
            role.ClaimRole.Add(new ClaimRole { ClaimsId = claim.Id, Claims = claim, RolesId = role.Id, Roles = role });
        }

        return role;
    }

    public static Identity GetRandomIdentity()
    {
        var identity = new Identity
        {
            Id = GetUniqueRandomId(UsedIdentityIds),
            Username = $"User{Random.Next(1000)}",
            Email = $"user{Random.Next(1000)}@example.com",
            PasswordSalt = $"salt{Random.Next(1000)}",
            PasswordHash = $"hash{Random.Next(1000)}",
            RegistrationDate = DateTime.UtcNow.AddDays(-Random.Next(365)),
            LastLogin = DateTime.UtcNow.AddDays(-Random.Next(30)),
            ClaimIdentities = new List<ClaimIdentity>(),
            IdentityRole = new List<IdentityRole>()
        };

        int numberOfClaims = Random.Next(3);
        for (int i = 0; i < numberOfClaims; i++)
        {
            var claim = GetRandomClaim();
            identity.ClaimIdentities.Add(new ClaimIdentity { Id = Random.Next(1, 1000), ClaimsId = claim.Id, Claims = claim });
        }

        int numberOfRoles = Random.Next(1, 3);
        for (int i = 0; i < numberOfRoles; i++)
        {
            var role = GetRandomRole();
            identity.IdentityRole.Add(new IdentityRole { Id = Random.Next(1, 1000), RolesId = role.Id, Roles = role });
        }

        return identity;
    }

    public static List<Claim> GetRandomClaims(int count)
    {
        return Enumerable.Range(0, count).Select(_ => GetRandomClaim()).ToList();
    }

    public static List<Role> GetRandomRoles(int count)
    {
        return Enumerable.Range(0, count).Select(_ => GetRandomRole()).ToList();
    }

    public static List<Identity> GetRandomIdentities(int count)
    {
        return Enumerable.Range(0, count).Select(_ => GetRandomIdentity()).ToList();
    }
}