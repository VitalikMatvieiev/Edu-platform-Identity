using Moq;
using Identity_Application.Queries.Identities;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;

namespace ApplicationUnitTests.Queries.Identities;

public class GetIdentityByIdHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetIdentityByIdHandler _handler;

    public GetIdentityByIdHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetIdentityByIdHandler(_mockIdentityRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldReturnIdentity()
    {
        // Arrange
        var identityId = 1;
        var expectedIdentity = new Identity { Id = identityId, Username = "test", Email = "test@gss.com" };

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(new List<Identity> { expectedIdentity });

        _mockMapper.Setup(m => m.Map<IdentityDTO>(It.IsAny<Identity>()))
                   .Returns((Identity src) => new IdentityDTO { Id = src.Id, Username = src.Username, Email = src.Email });

        // Act
        var result = await _handler.Handle(new GetIdentityByIdQuery(identityId), new CancellationToken());

        // Assert
        Assert.Equal(expectedIdentity.Id, result.Id);
        Assert.Equal(expectedIdentity.Username, result.Username);
        Assert.Equal(expectedIdentity.Email, result.Email);
    }

    [Fact]
    public async Task Handle_InvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidIdentityId = -1;

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(new List<Identity>());

        // Act
        var result = await _handler.Handle(new GetIdentityByIdQuery(invalidIdentityId), new CancellationToken());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var identityId = 1;
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetIdentityByIdQuery(identityId), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}