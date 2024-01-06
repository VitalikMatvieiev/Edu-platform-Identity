using Moq;
using Identity_Application.Commands.Roles;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Identity_Application.Models.BaseEntitiesModels;

namespace ApplicationUnitTests.Commands.Roles;

public class UpdateRoleHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly UpdateRoleHandler _handler;

    public UpdateRoleHandlerTests()
    {
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _handler = new UpdateRoleHandler(_mockRoleRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateRole()
    {
        // Arrange
        var roleId = 1;
        var updatedName = "UpdatedRoleName";
        var updateRoleCommand = new UpdateRoleCommand(roleId, new RoleVM { Name = updatedName, ClaimsIds = new int?[] { } });
        var existingRole = new Role { Id = roleId, Name = "OriginalName" };

        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, ""))
                           .ReturnsAsync(new List<Role> { existingRole });

        _mockRoleRepository.Setup(repo => repo
                           .UpdateAsync(It.IsAny<Role>()));

        // Act
        await _handler.Handle(updateRoleCommand, new CancellationToken());

        // Assert
        _mockRoleRepository.Verify(repo => repo
                           .UpdateAsync(It.Is<Role>(r => r.Id == roleId && r.Name == updatedName)), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        // Arrange
        var invalidRoleId = -1;
        var invalidCommand = new UpdateRoleCommand(invalidRoleId, new RoleVM { Name = "test", ClaimsIds = new int?[] { } });

        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, ""))
                           .ReturnsAsync(new List<Role>());

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
        var validCommand = new UpdateRoleCommand(validRoleId, new RoleVM { Name = "test", ClaimsIds = new int?[] { } });
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockRoleRepository.Setup(repo => repo
                           .UpdateAsync(It.IsAny<Role>()))
                           .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(validCommand, new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
        _mockRoleRepository.Verify(repo => repo
                           .UpdateAsync(It.IsAny<Role>()), Times.Once);
    }
}