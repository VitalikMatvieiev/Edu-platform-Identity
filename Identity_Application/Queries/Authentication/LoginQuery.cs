using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.AuthorizationModels;
using MediatR;

namespace Identity_Application.Queries.Authentication;

public record LoginQuery(LoginVM LoginVM) : IRequest<string>;

public class LoginHandler : IRequestHandler<LoginQuery, string>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IValidator<LoginVM> _validator;

    public LoginHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        _validator = new LoginValidator();
    }

    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        if (request.LoginVM is null)
            throw new ArgumentNullException("Provided login data was incorrect");

        var errors = _validator.Validate(request.LoginVM);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        string token;

        try
        {
            if (request.LoginVM.Username is null && request.LoginVM.Email is null)
                throw new ArgumentNullException("Provided login data cannot have empty both username and email");

            if (request.LoginVM.Username is null)
            {
                token = await _authenticationService
                    .LoginByEmail(request.LoginVM.Email, request.LoginVM.Password);

                return token;
            }

            token = await _authenticationService
                .LoginByUsername(request.LoginVM.Username, request.LoginVM.Password);
        }
        catch (Exception ex)
        {
            throw new Exception($"Registration exception occured: {ex.Message}", ex);
        }

        return token;
    }
}