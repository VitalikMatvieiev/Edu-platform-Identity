using Moq;
using Identity_Application.Commands.Identities;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Interfaces.Authentication;
using Identity_Domain.Entities.Base;
using Identity_Application.Models.AppSettingsModels;
using Microsoft.Extensions.Options;
using Identity_Application.Models.BaseEntitiesModels;

namespace ApplicationUnitTests.Commands.Identities;

public class EditIdentityHandlerTests
{
    private readonly Mock<IGenericRepository<Identity>> _mockIdentityRepository;
    private readonly Mock<IPasswordHasherService> _mockPasswordHasherService;
    private readonly Mock<IOptions<PasswordHashSettings>> _mockConfig;
    private readonly EditIdentityHandler _handler;

    public EditIdentityHandlerTests()
    {
        _mockIdentityRepository = new Mock<IGenericRepository<Identity>>();
        _mockPasswordHasherService = new Mock<IPasswordHasherService>();
        _mockConfig = new Mock<IOptions<PasswordHashSettings>>();
        _handler = new EditIdentityHandler(_mockIdentityRepository.Object, _mockPasswordHasherService.Object, _mockConfig.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateIdentity()
    {
        // Arrange
        var identityId = 1;
        var updatedUsername = "UpdatedUsername";
        var updateIdentityCommand = new UpdateIdentityCommand(identityId, new IdentityVM { Username = updatedUsername });
        var existingIdentity = new Identity { Id = identityId, Username = "OriginalUsername" };

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(null, null, ""))
                               .ReturnsAsync(new List<Identity> { existingIdentity });

        _mockIdentityRepository.Setup(repo => repo
                               .UpdateAsync(It.IsAny<Identity>()));

        // Act
        await _handler.Handle(updateIdentityCommand, CancellationToken.None);

        // Assert
        _mockIdentityRepository.Verify(repo => repo
                               .UpdateAsync(It.Is<Identity>(i => i.Id == identityId && i.Username == updatedUsername)), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        // Arrange
        var invalidIdentityId = -1;
        var invalidCommand = new UpdateIdentityCommand(invalidIdentityId, new IdentityVM { });

        _mockIdentityRepository.Setup(repo => repo
                               .GetAsync(null, null, ""))
                               .ReturnsAsync(new List<Identity>());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(invalidCommand, CancellationToken.None));

        Assert.Equal("Identity not found.", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        var validIdentityId = 2;
        var validCommand = new UpdateIdentityCommand(validIdentityId, new IdentityVM { });
        var expectedException = new InvalidOperationException("Error occurred during database operation.");

        _mockIdentityRepository.Setup(repo => repo
                               .UpdateAsync(It.IsAny<Identity>()))
                               .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(validCommand, CancellationToken.None));

        Assert.Equal(expectedException.Message, exception.Message);
        _mockIdentityRepository.Verify(repo => repo
                               .UpdateAsync(It.IsAny<Identity>()), Times.Once);
    }
}