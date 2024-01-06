using Moq;
using Identity_Application.Queries.Identities;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Identity_Domain.Entities.Additional;

namespace ApplicationUnitTests.Queries.Identities;

public class GetAllIdentitiesHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly GetAllIdentitiesHandler _handler;

    public GetAllIdentitiesHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _handler = new GetAllIdentitiesHandler(_mockIdentityRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfIdentities()
    {
        // Arrange
        var expectedIdentities = new List<Identity>
        {
            new Identity
            {
                Username = "testUser1",
                Email = "testUser1@example.com",
                PasswordSalt = "salt1",
                PasswordHash = "hash1",
                RegistrationDate = new DateTime(2022, 1, 1),
                LastLogin = new DateTime(2022, 6, 1),
                LastLogout = new DateTime(2022, 6, 10),
                Token = null,
                ClaimIdentities = new List<ClaimIdentity>(),
                IdentityRole = new List<IdentityRole>()
            },
            new Identity
            {
                Username = "testUser2",
                Email = "testUser2@example.com",
                PasswordSalt = "salt2",
                PasswordHash = "hash2",
                RegistrationDate = new DateTime(2022, 2, 1),
                LastLogin = new DateTime(2022, 6, 5),
                LastLogout = new DateTime(2022, 6, 15),
                Token = null,
                ClaimIdentities = new List<ClaimIdentity>(),
                IdentityRole = new List<IdentityRole>()
            }
        };

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(expectedIdentities);

        // Act
        var result = await _handler.Handle(new GetAllIdentitiesQuery(), new CancellationToken());

        // Assert
        Assert.Equal(expectedIdentities.Count, result.Count);
        Assert.Equal(expectedIdentities, result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Error occurred during database operation.");
        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetAllIdentitiesQuery(), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}