using Identity_Application.Commands.Authentication;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.Authorization;
using Moq;

namespace ApplicationUnitTests.Commands.Authentication;

public class RegisterHandlerTests
{
    private readonly Mock<IAuthenticationService> _mockAuthenticationService;
    private readonly RegisterHandler _handler;

    public RegisterHandlerTests()
    {
        _mockAuthenticationService = new Mock<IAuthenticationService>();
        _handler = new RegisterHandler(_mockAuthenticationService.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldRegisterUser()
    {
        // Arrange
        var registerVM = new RegisterVM
        {
            Username = "TestUser",
            Email = "test@example.com",
            Password = "password123"
        };

        var command = new RegisterCommand(registerVM);

        var expectedToken = "some-auth-token";

        _mockAuthenticationService.Setup(s => s
                                  .Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                  .ReturnsAsync(expectedToken);

        // Act
        var result = await _handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(expectedToken, result);
        _mockAuthenticationService.Verify(s => s
                                  .Register(It.IsAny<string>(), It.IsAny<string>(),
                                  It.IsAny<string>()), Times.Once);
    }
}