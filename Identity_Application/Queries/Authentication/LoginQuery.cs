using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.AuthorizationModels;
using MediatR;

namespace Identity_Application.Queries.Authentication;

public record LoginQuery(LoginVM LoginVM) : IRequest<string>;

public class LoginHandler : IRequestHandler<LoginQuery, string>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var token = await _authenticationService
            .Login(request.LoginVM.Email, request.LoginVM.Password);

        return token;
    }
}