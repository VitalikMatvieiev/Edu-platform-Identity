using Moq;
using Identity_Application.Queries.Roles;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Queries.Roles;

public class GetAllRolesHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly GetAllRolesHandler _handler;

    public GetAllRolesHandlerTests()
    {
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _handler = new GetAllRolesHandler(_mockRoleRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfRoles()
    {
        // Arrange
        var expectedRoles = new List<Role>
        {
            new Role { Name = "test" },
            new Role { Name = "test2" }
        };

        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, "ClaimRole,ClaimRole.Claims"))
                           .ReturnsAsync(expectedRoles);

        // Act
        var result = await _handler.Handle(new GetAllRolesQuery(), new CancellationToken());

        // Assert
        Assert.Equal(expectedRoles.Count, result.Count);
        Assert.Equal(expectedRoles, result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Error occurred during database operation.");
        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, "ClaimRole,ClaimRole.Claims"))
                           .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetAllRolesQuery(), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}