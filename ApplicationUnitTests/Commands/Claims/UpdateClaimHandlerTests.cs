using Moq;
using Identity_Application.Commands.Claims;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Identity_Application.Models.BaseEntitiesModels;

namespace ApplicationUnitTests.Commands.Claims;

public class UpdateClaimHandlerTests
{
    private readonly Mock<IGenericRepository<Claim>> _mockClaimRepository;
    private readonly UpdateClaimHandler _handler;

    public UpdateClaimHandlerTests()
    {
        _mockClaimRepository = new Mock<IGenericRepository<Claim>>();
        _handler = new UpdateClaimHandler(_mockClaimRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateClaim()
    {
        // Arrange
        var claimId = 1;
        var existingClaim = new Claim { Id = claimId, Name = "OriginalName" };

        var updatedName = "UpdatedClaimName";
        var updateClaimCommand = new UpdateClaimCommand(claimId, new ClaimVM { Name = updatedName });

        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Claim, bool>>>(), null, ""))
                            .ReturnsAsync(new List<Claim> { existingClaim });

        _mockClaimRepository.Setup(repo => repo
                            .UpdateAsync(It.IsAny<Claim>()));

        // Act
        await _handler.Handle(updateClaimCommand, CancellationToken.None);

        // Assert
        _mockClaimRepository.Verify(repo => repo
                            .UpdateAsync(It.Is<Claim>(c => c.Id == claimId && c.Name == updatedName)), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        // Arrange
        var invalidClaimId = -1;
        var invalidCommand = new UpdateClaimCommand(invalidClaimId, new ClaimVM { Name = "Name" });

        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Claim, bool>>>(), null, ""))
                            .ReturnsAsync(new List<Claim>());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(invalidCommand, CancellationToken.None));

        Assert.Equal("Claim not found.", exception.Message);
    }
}