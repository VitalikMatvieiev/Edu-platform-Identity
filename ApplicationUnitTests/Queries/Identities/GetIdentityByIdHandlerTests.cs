using Moq;
using Identity_Application.Queries.Identities;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Queries.Identities;

public class GetIdentityByIdHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly GetIdentityByIdHandler _handler;

    public GetIdentityByIdHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _handler = new GetIdentityByIdHandler(_mockIdentityRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldReturnIdentity()
    {
        // Arrange
        var identityId = 1;
        var expectedIdentity = new Identity { Id = identityId, Username = "test", Email = "test@gss.com" };

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(new List<Identity> { expectedIdentity });

        // Act
        var result = await _handler.Handle(new GetIdentityByIdQuery(identityId), new CancellationToken());

        // Assert
        Assert.Equal(expectedIdentity, result);
    }

    [Fact]
    public async Task Handle_InvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidIdentityId = -1;

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(new List<Identity>());

        // Act
        var result = await _handler.Handle(new GetIdentityByIdQuery(invalidIdentityId), new CancellationToken());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var identityId = 1;
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetIdentityByIdQuery(identityId), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}