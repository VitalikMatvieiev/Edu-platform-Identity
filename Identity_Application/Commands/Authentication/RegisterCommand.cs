using Identity_Application.Interfaces.Authentication;
using MediatR;

namespace Identity_Application.Commands.Authentication;

public record RegisterCommand(string Username, string Email, string Password) : IRequest<string>;

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var token = await _authenticationService.Register(request.Username, request.Email, request.Password);

        return token;
    }
}