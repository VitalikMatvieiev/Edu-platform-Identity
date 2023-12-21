using Identity_Domain.Entities.Base;
using Identity_Infrastructure;
using Identity_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InfrastructureUnitTests.Repositories;

public class GenericRepositoryTests
{
    private readonly Mock<DbSet<Claim>> _mockSet;
    private readonly Mock<IdentityDbContext> _mockContext;
    private readonly GenericRepository<Claim> _repository;
    private IQueryable<Claim> data;

    public GenericRepositoryTests()
    {
        _mockSet = new Mock<DbSet<Claim>>();

        // Setup your in-memory data
        data = TestDataGenerator.GetRandomClaims(3).AsQueryable();

        //_mockSet.As<IQueryable<Claim>>()
        //    .Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Claim>(data.Provider));

        //_mockSet.As<IQueryable<Claim>>()
        //    .Setup(m => m.Expression).Returns(data.Expression);

        //_mockSet.As<IQueryable<Claim>>()
        //    .Setup(m => m.ElementType).Returns(data.ElementType);

        //_mockSet.As<IQueryable<Claim>>()
        //    .Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        _mockContext = new Mock<IdentityDbContext>();
        _mockContext.Setup(m => m.Set<Claim>()).Returns(_mockSet.Object);

        _repository = new GenericRepository<Claim>(_mockContext.Object);
    }

    [Fact]
    public async Task InsertAsync_AddsEntity()
    {
        // Arrange
        var claim = TestDataGenerator.GetRandomClaim();

        _mockSet.Setup(m => m.AddAsync(It.IsAny<Claim>(), It.IsAny<CancellationToken>()))
                .Callback<Claim, CancellationToken>((entity, token) =>
                    data.Append(entity)); // Simulates adding the entity to the data source

        // Act
        await _repository.InsertAsync(claim);

        // Assert
        _mockSet.Verify(m => m
            .AddAsync(It.IsAny<Claim>(), It.IsAny<CancellationToken>()), Times.Once);

        _mockContext.Verify(m => m
            .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_EntityUpdated()
    {
        // Arrange
        var claim = data.First();

        claim.Name = "www";

        // Act
        await _repository.UpdateAsync(claim);

        // Assert
        _mockContext.Verify(m =>
            m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once); // Verifies that changes are saved
    }
}