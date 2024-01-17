using Identity_Application.Commands.Claims;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using Moq;

namespace ApplicationUnitTests.Commands.Claims;

public class CreateClaimHandlerTests
{
    private readonly Mock<IGenericRepository<Claim>> _mockClaimRepository;
    private readonly CreateClaimHandler _handler;

    public CreateClaimHandlerTests()
    {
        _mockClaimRepository = new Mock<IGenericRepository<Claim>>();
        _handler = new CreateClaimHandler(_mockClaimRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateClaim()
    {
        // Arrange
        var claimName = "TestClaim";

        var vm = new ClaimVM
        {
            Name = claimName
        };

        var createClaimCommand = new CreateClaimCommand(vm);

        var expectedClaim = new Claim { Name = claimName };

        _mockClaimRepository.Setup(repo => repo
                            .InsertAsync(It.IsAny<Claim>()));

        // Act
        var result = await _handler.Handle(createClaimCommand, CancellationToken.None);

        // Assert
        _mockClaimRepository.Verify(repo => repo
                            .InsertAsync(It.IsAny<Claim>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsArgumentException()
    {
        // Arrange
        // Assuming Name is required and cannot be null
        var invalidCommand = new CreateClaimCommand(new ClaimVM { Name = null! });

        // Simulate repository validation
        _mockClaimRepository.Setup(repo => repo
                            .InsertAsync(It.IsAny<Claim>()))
                            .Throws(new ArgumentException("Name cannot be null"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(invalidCommand, CancellationToken.None));

        Assert.Equal("Name cannot be null", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var validCommand = new CreateClaimCommand(new ClaimVM { Name = "ValidName" });
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockClaimRepository.Setup(repo => repo
                            .InsertAsync(It.IsAny<Claim>()))
                            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(validCommand, CancellationToken.None));

        Assert.Equal(expectedException.Message, exception.Message);
        _mockClaimRepository.Verify(repo => repo
                            .InsertAsync(It.IsAny<Claim>()), Times.Once);
    }
}