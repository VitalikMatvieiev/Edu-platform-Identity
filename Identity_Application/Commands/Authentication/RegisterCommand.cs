using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.AuthorizationModels;
using MediatR;

namespace Identity_Application.Commands.Authentication;

public record RegisterCommand(RegisterVM vm) : IRequest<string>;

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.vm is null)
            throw new ArgumentNullException("Given data is not correct");

        var token = await _authenticationService.Register(request.vm.Username, request.vm.Email, request.vm.Password);

        return token;
    }
}