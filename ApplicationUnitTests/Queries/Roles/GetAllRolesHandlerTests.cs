using Moq;
using Identity_Application.Queries.Roles;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;

namespace ApplicationUnitTests.Queries.Roles;

public class GetAllRolesHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAllRolesHandler _handler;

    public GetAllRolesHandlerTests()
    {
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetAllRolesHandler(_mockRoleRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfRoles()
    {
        // Arrange
        var expectedRoles = new List<Role>
        {
            new () { Name = "test" },
            new () { Name = "test2" }
        };

        _mockRoleRepository.Setup(repo => repo
                           .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>(), null, "ClaimRole,ClaimRole.Claims"))
                           .ReturnsAsync(expectedRoles);

        _mockMapper.Setup(m => m.Map<RoleDTO>(It.IsAny<Role>()))
                   .Returns((Role src) => new RoleDTO { Id = src.Id, Name = src.Name });

        // Act
        var result = await _handler.Handle(new GetAllRolesQuery(), new CancellationToken());

        // Assert
        Assert.Equal(expectedRoles.Count, result.Count);
        Assert.Equal(expectedRoles[0].Name, result[0].Name);
        Assert.Equal(expectedRoles[1].Name, result[1].Name);
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