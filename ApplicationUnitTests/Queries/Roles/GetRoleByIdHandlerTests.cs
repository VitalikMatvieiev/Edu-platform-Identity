using Moq;
using Identity_Application.Queries.Roles;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Queries.Roles;

public class GetRoleByIdHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly GetRoleByIdHandler _handler;

    public GetRoleByIdHandlerTests()
    {
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _handler = new GetRoleByIdHandler(_mockRoleRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldReturnRole()
    {
        // Arrange
        var roleId = 1;
        var expectedRole = new Role { Id = roleId, /* other properties */ };
        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, "ClaimRole.Claims"))
                           .ReturnsAsync(new List<Role> { expectedRole });

        // Act
        var result = await _handler.Handle(new GetRoleByIdQuery(roleId), new CancellationToken());

        // Assert
        Assert.Equal(expectedRole, result);
    }

    [Fact]
    public async Task Handle_InvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidRoleId = -1;
        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, "ClaimRole.Claims"))
                           .ReturnsAsync(new List<Role>());

        // Act
        var result = await _handler.Handle(new GetRoleByIdQuery(invalidRoleId), new CancellationToken());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var roleId = 1;
        var expectedException = new InvalidOperationException("Error occurred during database operation.");
        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, "ClaimRole.Claims"))
                           .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetRoleByIdQuery(roleId), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}