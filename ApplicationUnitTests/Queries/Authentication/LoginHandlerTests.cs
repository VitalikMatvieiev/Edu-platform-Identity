using Moq;
using Identity_Application.Queries.Authentication;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.AuthorizationModels;

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
    public async Task Handle_ValidCredentialsWithEmail_ShouldReturnToken()
    {
        // Arrange
        var validLoginVM = new LoginVM { Email = "test@example.com", Password = "password" };
        var expectedToken = "ValidToken";
        var loginQuery = new LoginQuery(validLoginVM);

        _mockAuthenticationService.Setup(service => service
                                  .LoginByEmail(validLoginVM.Email, validLoginVM.Password))
                                  .ReturnsAsync(expectedToken);

        // Act
        var result = await _handler.Handle(loginQuery, CancellationToken.None);

        // Assert
        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task Handle_ValidCredentialsWithUsername_ShouldReturnToken()
    {
        // Arrange
        var validLoginVM = new LoginVM { Username = "testusername", Password = "password" };
        var expectedToken = "ValidToken";
        var loginQuery = new LoginQuery(validLoginVM);

        _mockAuthenticationService.Setup(service => service
                                  .LoginByUsername(validLoginVM.Username, validLoginVM.Password))
                                  .ReturnsAsync(expectedToken);

        // Act
        var result = await _handler.Handle(loginQuery, CancellationToken.None);

        // Assert
        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task Handle_InvalidCredentialsNoUsernameAndPassword_ShouldThrowException()
    {
        // Arrange
        var invalidLoginVM = new LoginVM { Password = "invalidPassword" };
        var loginQuery = new LoginQuery(invalidLoginVM);

        _mockAuthenticationService.Setup(service => service
                                  .LoginByEmail(invalidLoginVM.Email, invalidLoginVM.Password))
                                  .ThrowsAsync(new Exception("Provided login data cannot have empty both username and email"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(loginQuery, CancellationToken.None));

        Assert.Contains("Provided login data cannot have empty both username and email", exception.Message);
    }

    [Fact]
    public async Task Handle_InvalidCredentials_ShouldThrowException()
    {
        // Arrange
        var invalidLoginVM = new LoginVM { Email = "invalid@example.com", Password = "invalidPassword" };
        var loginQuery = new LoginQuery(invalidLoginVM);

        _mockAuthenticationService.Setup(service => service
                                  .LoginByEmail(invalidLoginVM.Email, invalidLoginVM.Password))
                                  .ThrowsAsync(new Exception("Registration exception occured:"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(loginQuery, CancellationToken.None));

        Assert.Contains("Registration exception occured:", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenAuthenticationServiceThrowsException_ShouldThrow()
    {
        // Arrange
        var loginVM = new LoginVM { Email = "test@example.com", Password = "password" };
        var loginQuery = new LoginQuery(loginVM);
        var expectedException = new Exception("Registration exception occured:");

        _mockAuthenticationService.Setup(service => service
                                  .LoginByEmail(loginVM.Email, loginVM.Password))
                                  .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(loginQuery, CancellationToken.None));

        Assert.Contains(expectedException.Message, exception.Message);
    }
}