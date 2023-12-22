using Moq;
using Identity_Application.Queries.Authentication;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.Authorization;

namespace ApplicationUnitTests.Queries.Authentication;

public class LoginHandlerTests
{
    private readonly Mock<IAuthenticationService> _mockAuthenticationService;
    private readonly LoginHandler _handler;

    public LoginHandlerTests()
    {
        _mockAuthenticationService = new Mock<IAuthenticationService>();
        _handler = new LoginHandler(_mockAuthenticationService.Object);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ShouldReturnToken()
    {
        // Arrange
        var validLoginVM = new LoginVM { Email = "test@example.com", Password = "password" };
        var expectedToken = "ValidToken";
        var loginQuery = new LoginQuery(validLoginVM);

        _mockAuthenticationService.Setup(service => service
                                  .Login(validLoginVM.Email, validLoginVM.Password))
                                  .ReturnsAsync(expectedToken);

        // Act
        var result = await _handler.Handle(loginQuery, new CancellationToken());

        // Assert
        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task Handle_InvalidCredentials_ShouldThrowException()
    {
        // Arrange
        var invalidLoginVM = new LoginVM { Email = "invalid@example.com", Password = "invalidPassword" };
        var loginQuery = new LoginQuery(invalidLoginVM);

        _mockAuthenticationService.Setup(service => service
                                  .Login(invalidLoginVM.Email, invalidLoginVM.Password))
                                  .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _handler.Handle(loginQuery, new CancellationToken()));

        Assert.Equal("Invalid credentials", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenAuthenticationServiceThrowsException_ShouldThrow()
    {
        // Arrange
        var loginVM = new LoginVM { Email = "test@example.com", Password = "password" };
        var loginQuery = new LoginQuery(loginVM);
        var expectedException = new InvalidOperationException("Authentication service error");

        _mockAuthenticationService.Setup(service => service
                                  .Login(loginVM.Email, loginVM.Password))
                                  .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(loginQuery, new CancellationToken()));

        Assert.Equal(expectedException.Message, exception.Message);
    }
}