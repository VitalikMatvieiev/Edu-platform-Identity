using Identity_Application.Commands.Identities;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.AppSettingsModels;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Additional;
using Identity_Domain.Entities.Base;
using Microsoft.Extensions.Options;
using Moq;

namespace ApplicationUnitTests.Commands.Identities;

public class CreateIdentityHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly Mock<IPasswordHasherService> _mockPasswordHasher;
    private readonly Mock<IOptions<PasswordHashSettings>> _mockConfig;
    private readonly CreateIdentityHandler _handler;

    public CreateIdentityHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _mockPasswordHasher = new Mock<IPasswordHasherService>();
        _mockConfig = new Mock<IOptions<PasswordHashSettings>>();
        _handler = new CreateIdentityHandler(_mockIdentityRepository.Object,
                                             _mockPasswordHasher.Object,
                                             _mockConfig.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateIdentity()
    {
        // Arrange
        var vm = new IdentityVM
        {
            Username = "NewUser",
            Email = "newuser@example.com",
            Password = "password123",
            ClaimsIds = new int?[] { 1, 2 },
            RolesIds = new int?[] { 1 }
        };

        var command = new CreateIdentityCommand(vm);

        var salt = "random_salt";
        var hash = "hashed_password";

        var expectedIdentity = new Identity
        {
            Username = "NewUser",
            Email = "newuser@example.com",
            PasswordSalt = salt,
            PasswordHash = hash
        };

        foreach (var claim in command.IdentityVM.ClaimsIds)
            expectedIdentity.ClaimIdentities.Add(
                new ClaimIdentity
                {
                    Identities = expectedIdentity,
                    ClaimsId = claim
                });

        foreach (var role in command.IdentityVM.RolesIds)
            expectedIdentity.IdentityRole.Add(
                new IdentityRole
                {
                    Identities = expectedIdentity,
                    RolesId = role
                });

        _mockIdentityRepository.Setup(repo => repo
                               .InsertAsync(It.IsAny<Identity>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockIdentityRepository.Verify(repo => repo
                               .InsertAsync(It.IsAny<Identity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsArgumentException()
    {
        // Arrange
        var invalidCommand = new CreateIdentityCommand(
            new IdentityVM
            {
                // Invalid Username is required
                Username = null,
                Email = "test@example.com",
                Password = "password123",
                ClaimsIds = new int?[] { 1, 2 },
                RolesIds = new int?[] { 1 }
            });

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(invalidCommand, CancellationToken.None));

        // Assuming exception message contains field name
        Assert.Contains("Username", exception.Message);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var command = new CreateIdentityCommand(
            new IdentityVM
            {
                Username = "TestUser",
                Email = "test@example.com",
                Password = "password123",
                ClaimsIds = new int?[] { 1, 2 },
                RolesIds = new int?[] { 1 }
            });

        var expectedException = new Exception("Database error occurred.");

        _mockIdentityRepository.Setup(repo => repo
                               .InsertAsync(It.IsAny<Identity>()))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}