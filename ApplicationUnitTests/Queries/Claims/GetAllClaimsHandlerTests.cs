using Moq;
using Identity_Application.Queries.Claims;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Queries.Claims;

public class GetAllClaimsHandlerTests
{
    private readonly Mock<IGenericRepository<Claim>> _mockClaimRepository;
    private readonly GetAllClaimsHandler _handler;

    public GetAllClaimsHandlerTests()
    {
        _mockClaimRepository = new Mock<IGenericRepository<Claim>>();
        _handler = new GetAllClaimsHandler(_mockClaimRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfClaims()
    {
        // Arrange
        var expectedClaims = new List<Claim>
        {
            new Claim { Id = 1, Name = "test1" },
            new Claim { Id = 2, Name = "test2" }
        };

        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(null, null, ""))
                            .ReturnsAsync(expectedClaims);

        // Act
        var result = await _handler.Handle(new GetAllClaimsQuery(), new CancellationToken());

        // Assert
        Assert.Equal(expectedClaims.Count, result.Count);
        Assert.Equal(expectedClaims, result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(null, null, ""))
                            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetAllClaimsQuery(), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}