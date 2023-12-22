using Moq;
using Identity_Application.Commands.Roles;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Commands.Roles;

public class DeleteRoleHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly DeleteRoleHandler _handler;

    public DeleteRoleHandlerTests()
    {
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _handler = new DeleteRoleHandler(_mockRoleRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldDeleteRole()
    {
        // Arrange
        var roleId = 1;
        var deleteRoleCommand = new DeleteRoleCommand(roleId);

        _mockRoleRepository.Setup(repo => repo
                           .DeleteAsync(roleId));

        // Act
        await _handler.Handle(deleteRoleCommand, new CancellationToken());

        // Assert
        _mockRoleRepository.Verify(repo => repo
                           .DeleteAsync(roleId), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        // Arrange
        var invalidRoleId = -1;
        var invalidCommand = new DeleteRoleCommand(invalidRoleId);

        _mockRoleRepository.Setup(repo => repo
                           .DeleteAsync(invalidRoleId))
                           .ThrowsAsync(new KeyNotFoundException("Role not found."));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(invalidCommand, new CancellationToken()));

        Assert.Equal("Role not found.", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var validRoleId = 2;
        var validCommand = new DeleteRoleCommand(validRoleId);
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockRoleRepository.Setup(repo => repo
                           .DeleteAsync(validRoleId))
                           .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(validCommand, new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
        _mockRoleRepository.Verify(repo => repo
                           .DeleteAsync(validRoleId), Times.Once);
    }
}