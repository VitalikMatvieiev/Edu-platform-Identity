using Moq;
using Identity_Application.Commands.Claims;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Commands.Claims;

public class DeleteClaimHandlerTests
{
    private readonly Mock<IGenericRepository<Claim>> _mockClaimRepository;
    private readonly DeleteClaimHandler _handler;

    public DeleteClaimHandlerTests()
    {
        _mockClaimRepository = new Mock<IGenericRepository<Claim>>();
        _handler = new DeleteClaimHandler(_mockClaimRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldDeleteClaim()
    {
        // Arrange
        var claimId = 1;

        var deleteClaimCommand = new DeleteClaimCommand(claimId);

        _mockClaimRepository.Setup(repo => repo
                            .DeleteAsync(claimId));

        // Act
        await _handler.Handle(deleteClaimCommand, new CancellationToken());

        // Assert
        _mockClaimRepository.Verify(repo => repo
                            .DeleteAsync(claimId), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        // Arrange
        var invalidClaimId = -1;
        var invalidCommand = new DeleteClaimCommand(invalidClaimId);

        _mockClaimRepository.Setup(repo => repo
                            .DeleteAsync(invalidClaimId))
                            .ThrowsAsync(new KeyNotFoundException("Claim not found."));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(invalidCommand, new CancellationToken()));

        Assert.Equal("Claim not found.", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var validClaimId = 2;
        var validCommand = new DeleteClaimCommand(validClaimId);
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockClaimRepository.Setup(repo => repo
                            .DeleteAsync(validClaimId))
                            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(validCommand, new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
        _mockClaimRepository.Verify(repo => repo
                            .DeleteAsync(validClaimId), Times.Once);
    }
}