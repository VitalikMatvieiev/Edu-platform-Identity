using Moq;
using Identity_Application.Queries.Claims;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Identity_Application.Models.BaseEntitiesDTOs;
using AutoMapper;

namespace ApplicationUnitTests.Queries.Claims;

public class GetAllClaimsHandlerTests
{
    private readonly Mock<IGenericRepository<Claim>> _mockClaimRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAllClaimsHandler _handler;

    public GetAllClaimsHandlerTests()
    {
        _mockClaimRepository = new Mock<IGenericRepository<Claim>>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetAllClaimsHandler(_mockClaimRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfClaims()
    {
        // Arrange
        var expectedClaims = new List<Claim>
        {
            new () { Id = 1, Name = "test1" },
            new () { Id = 2, Name = "test2" }
        };

        var expectedDTOs = expectedClaims.Select(c => new ClaimDTO { Id = c.Id, Name = c.Name }).ToList();

        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Claim, bool>>>(), null, ""))
                            .ReturnsAsync(expectedClaims);

        _mockMapper.Setup(m => m.Map<ClaimDTO>(It.IsAny<Claim>()))
                   .Returns((Claim src) => new ClaimDTO { Id = src.Id, Name = src.Name });

        // Act
        var result = await _handler.Handle(new GetAllClaimsQuery(), new CancellationToken());

        // Assert
        Assert.Equal(expectedDTOs.Count, result.Count);

        Assert.Equal(expectedDTOs[0].Id, result[0].Id);
        Assert.Equal(expectedDTOs[0].Name, result[0].Name);
        Assert.Equal(expectedDTOs[1].Id, result[1].Id);
        Assert.Equal(expectedDTOs[1].Name, result[1].Name);
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