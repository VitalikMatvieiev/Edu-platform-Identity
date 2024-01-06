using Application.UnitTests;
using AutoFixture;
using Identity_Domain.Entities.Base;
using Identity_Infrastructure;
using Identity_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace InfrastructureUnitTests.Repositories;

public class GenericRepositoryTests
{
    /*private readonly Mock<DbSet<Claim>> _mockSet;
    private readonly Mock<IdentityDbContext> _mockContext;
    private readonly GenericRepository<Claim> _repository;
    private IQueryable<Claim> data;

    public GenericRepositoryTests()
    {
        _mockSet = new Mock<DbSet<Claim>>();

        // Setup your in-memory data
        data = TestDataGenerator.GetRandomClaims(3).AsQueryable();

        _mockContext = new Mock<IdentityDbContext>();
        _mockContext.Setup(m => m.Set<Claim>()).Returns(_mockSet.Object);

        _repository = new GenericRepository<Claim>(_mockContext.Object);
    }*/

    [Theory]
    [AutoMoqData]
    public async Task InsertAsync_AddsEntity(IFixture fixture)
    {
        // Arrange
        var newentity = new Mock<EntityEntry<Claim>>();

        var dbContextMock = new Mock<IdentityDbContext>();
        var dbSetMock = new Mock<DbSet<Claim>>();
        dbContextMock.Setup(c => c.Set<Claim>()).Returns(dbSetMock.Object);

        var sut = new GenericRepository<Claim>(dbContextMock.Object);
        var data = TestDataGenerator.GetRandomClaims(3).AsQueryable();
        var claim = TestDataGenerator.GetRandomClaim();

        dbSetMock.Setup(m => m.AddAsync(It.IsAny<Claim>(), It.IsAny<CancellationToken>())).ReturnsAsync(newentity.Object);
                //.Callback<Claim, CancellationToken>((entity, token) =>
                   // data.Append(entity))
                   // .Returns((Claim model, CancellationToken token) => 
                   // ValueTask.FromResult((EntityEntry<Claim>)null));

        // Act
        await sut.InsertAsync(claim);

        // Assert
        dbSetMock.Verify(m => m
            .AddAsync(It.IsAny<Claim>(), It.IsAny<CancellationToken>()), Times.Once);

        dbContextMock.Verify(m => m
            .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}