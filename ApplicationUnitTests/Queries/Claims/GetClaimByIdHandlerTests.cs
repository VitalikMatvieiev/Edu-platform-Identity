﻿using Moq;
using Identity_Application.Queries.Claims;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;

namespace ApplicationUnitTests.Queries.Claims;

public class GetClaimByIdHandlerTests
{
    private readonly Mock<IGenericRepository<Claim>> _mockClaimRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetClaimByIdHandler _handler;

    public GetClaimByIdHandlerTests()
    {
        _mockClaimRepository = new Mock<IGenericRepository<Claim>>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetClaimByIdHandler(_mockClaimRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldReturnClaim()
    {
        // Arrange
        var claimId = 1;
        var expectedClaim = new Claim { Id = claimId, Name = "test" };

        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Claim, bool>>>(), null, ""))
                            .ReturnsAsync(new List<Claim> { expectedClaim });

        _mockMapper.Setup(m => m.Map<ClaimDTO>(It.IsAny<Claim>()))
                   .Returns((Claim src) => new ClaimDTO { Id = src.Id, Name = src.Name });

        // Act
        var result = await _handler.Handle(new GetClaimByIdQuery(claimId), new CancellationToken());

        // Assert
        Assert.Equal(expectedClaim.Id, result.Id);
        Assert.Equal(expectedClaim.Name, result.Name);
    }

    [Fact]
    public async Task Handle_InvalidId_ShouldReturnNull()
    {
        // Arrange
        var invalidClaimId = -1;
        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Claim, bool>>>(), null, ""))
                            .ReturnsAsync(new List<Claim>());

        // Act
        var result = await _handler.Handle(new GetClaimByIdQuery(invalidClaimId), new CancellationToken());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var claimId = 1;
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockClaimRepository.Setup(repo => repo
                            .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Claim, bool>>>(), null, ""))
                            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetClaimByIdQuery(claimId), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}