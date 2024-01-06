using Moq;
using Identity_Application.Queries.Identities;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Queries.Identities;

public class GetIdentityByEmailHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly GetIdentityByEmailHandler _handler;

    public GetIdentityByEmailHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _handler = new GetIdentityByEmailHandler(_mockIdentityRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidEmail_ShouldReturnIdentity()
    {
        // Arrange
        var email = "test@example.com";
        var expectedIdentity = new Identity { Email = email /* other properties */ };
        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(new List<Identity> { expectedIdentity });

        // Act
        var result = await _handler.Handle(new GetIdentityByEmailQuery(email), new CancellationToken());

        // Assert
        Assert.Equal(expectedIdentity, result);
    }

    [Fact]
    public async Task Handle_InvalidEmail_ShouldReturnNull()
    {
        // Arrange
        var invalidEmail = "nonexistent@example.com";
        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(new List<Identity>());

        // Act
        var result = await _handler.Handle(new GetIdentityByEmailQuery(invalidEmail), new CancellationToken());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var email = "test@example.com";
        var expectedException = new InvalidOperationException("Error occurred during database operation.");
        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetIdentityByEmailQuery(email), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}