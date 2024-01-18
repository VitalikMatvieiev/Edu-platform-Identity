using Moq;
using Identity_Application.Queries.Identities;
using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Identity_Domain.Entities.Additional;
using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;
using FluentAssertions.Execution;
using FluentAssertions;

namespace ApplicationUnitTests.Queries.Identities;

public class GetAllIdentitiesHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAllIdentitiesHandler _handler;

    public GetAllIdentitiesHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetAllIdentitiesHandler(_mockIdentityRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfIdentities()
    {
        // Arrange
        var expectedIdentities = GetListOfIdentities();

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ReturnsAsync(expectedIdentities);

        _mockMapper.Setup(m => m.Map<IdentityDTO>(It.IsAny<Identity>()))
                   .Returns((Identity src) => 
                   new IdentityDTO 
                   { 
                       Id = src.Id, 
                       Username = src.Username,
                       Email = src.Email,
                       PasswordSalt = src.PasswordSalt,
                       PasswordHash = src.PasswordHash,
                       RegistrationDate = src.RegistrationDate,
                       LastLogin = src.LastLogin,
                       LastLogout = src.LastLogout
                   });

        // Act
        var result = await _handler.Handle(new GetAllIdentitiesQuery(), new CancellationToken());

        // Assert
        using (new AssertionScope())
        {
            result.Should().HaveCount(expectedIdentities.Count);

            expectedIdentities.Zip(result, (expected, actual) => new { Expected = expected, Actual = actual })
                              .ToList()
                              .ForEach(pair =>
                              {
                                  pair.Actual.Username.Should().Be(pair.Expected.Username);
                                  pair.Actual.Email.Should().Be(pair.Expected.Email);
                                  pair.Actual.PasswordSalt.Should().Be(pair.Expected.PasswordSalt);
                                  pair.Actual.PasswordHash.Should().Be(pair.Expected.PasswordHash);
                                  pair.Actual.RegistrationDate.Should().Be(pair.Expected.RegistrationDate);
                                  pair.Actual.LastLogin.Should().Be(pair.Expected.LastLogin);
                                  pair.Actual.LastLogout.Should().Be(pair.Expected.LastLogout);
                              });
        }
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Error occurred during database operation.");
        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Identity, bool>>>(), null, "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims"))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new GetAllIdentitiesQuery(), new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }

    private List<Identity> GetListOfIdentities()
    {
        return new List<Identity>()
        {
            new Identity
            {
                Username = "testUser1",
                Email = "testUser1@example.com",
                PasswordSalt = "salt1",
                PasswordHash = "hash1",
                RegistrationDate = new DateTime(2022, 1, 1),
                LastLogin = new DateTime(2022, 6, 1),
                LastLogout = new DateTime(2022, 6, 10),
                Token = null,
                ClaimIdentities = new List<ClaimIdentity>(),
                IdentityRole = new List<IdentityRole>()
            },
            new Identity
            {
                Username = "testUser2",
                Email = "testUser2@example.com",
                PasswordSalt = "salt2",
                PasswordHash = "hash2",
                RegistrationDate = new DateTime(2022, 2, 1),
                LastLogin = new DateTime(2022, 6, 5),
                LastLogout = new DateTime(2022, 6, 15),
                Token = null,
                ClaimIdentities = new List<ClaimIdentity>(),
                IdentityRole = new List<IdentityRole>()
            }
        };
    }
}