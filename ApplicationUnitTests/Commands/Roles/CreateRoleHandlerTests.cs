using Identity_Application.Commands.Roles;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using Moq;

namespace ApplicationUnitTests.Commands.Roles;

public class CreateRoleHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly CreateRoleHandler _handler;

    public CreateRoleHandlerTests()
    {
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _handler = new CreateRoleHandler(_mockRoleRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateRole()
    {
        // Arrange
        var roleVM = new RoleVM
        {
            Name = "Admin",
            ClaimsIds = new int?[] { 1, 2 }
        };

        var command = new CreateRoleCommand(roleVM);

        // Assuming this is the ID of the newly created role
        var expectedRoleId = 1; 

        _mockRoleRepository.Setup(repo => repo
                           .InsertAsync(It.IsAny<Role>()))
                           .ReturnsAsync(expectedRoleId);

        // Act
        var result = await _handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(expectedRoleId, result);
        _mockRoleRepository.Verify(repo => repo
                           .InsertAsync(It.IsAny<Role>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsArgumentException()
    {
        // Arrange
        // Invalid due to null name
        var invalidRoleVM = new RoleVM 
        { 
            Name = null, 
            ClaimsIds = new int?[] { 1 } 
        }; 
        var invalidCommand = new CreateRoleCommand(invalidRoleVM);

        // Simulate repository validation
        _mockRoleRepository.Setup(repo => repo
                           .InsertAsync(It.IsAny<Role>()))
                           .Throws(new ArgumentException("Name cannot be null")); 

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(invalidCommand, new CancellationToken()));

        // Check that exception message contains reference to the invalid field
        Assert.Contains("Name", exception.Message); 
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var validRoleVM = new RoleVM 
        { 
            Name = "Admin", 
            ClaimsIds = new int?[] { 1, 2 } 
        };

        var validCommand = new CreateRoleCommand(validRoleVM);
        var expectedException = new Exception("Database error occurred.");

        _mockRoleRepository.Setup(repo => repo
                           .InsertAsync(It.IsAny<Role>()))
                           .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(validCommand, new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);

        _mockRoleRepository.Verify(repo => repo
                           .InsertAsync(It.IsAny<Role>()), Times.Once);
    }
}