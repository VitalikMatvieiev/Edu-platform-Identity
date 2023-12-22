using Moq;
using Identity_Application.Commands.Identities;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Commands.Identities;

public class DeleteIdentityHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly DeleteIdentityHandler _handler;

    public DeleteIdentityHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _handler = new DeleteIdentityHandler(_mockIdentityRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldDeleteIdentity()
    {
        // Arrange
        var identityId = 1;
        var deleteIdentityCommand = new DeleteIdentityCommand(identityId);

        _mockIdentityRepository.Setup(repo => repo
                               .DeleteAsync(identityId));

        // Act
        await _handler.Handle(deleteIdentityCommand, new CancellationToken());

        // Assert
        _mockIdentityRepository.Verify(repo => repo
                               .DeleteAsync(identityId), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        // Arrange
        var invalidIdentityId = -1;
        var invalidCommand = new DeleteIdentityCommand(invalidIdentityId);

        _mockIdentityRepository.Setup(repo => repo
                               .DeleteAsync(invalidIdentityId))
                               .ThrowsAsync(new KeyNotFoundException("Identity not found."));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(invalidCommand, new CancellationToken()));

        Assert.Equal("Identity not found.", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var validIdentityId = 2;
        var validCommand = new DeleteIdentityCommand(validIdentityId);
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockIdentityRepository.Setup(repo => repo
                               .DeleteAsync(validIdentityId))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(validCommand, new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
        _mockIdentityRepository.Verify(repo => repo
                               .DeleteAsync(validIdentityId), Times.Once);
    }
}