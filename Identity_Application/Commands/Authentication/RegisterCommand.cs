using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.AuthorizationModels;
using MediatR;

namespace Identity_Application.Commands.Authentication;

public record RegisterCommand(RegisterVM RegisterVM) : IRequest<string>;

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IValidator<RegisterVM> _validator;

    public RegisterHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        _validator = new RegisterValidator();
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.RegisterVM is null)
            throw new ArgumentNullException("Provided registration data is incorrect");

        var errors = _validator.Validate(request.RegisterVM);

        foreach ( var error in errors.Errors )
        {
            throw new Exception(error.ErrorMessage);
        }

        string token;

        try
        {
            token = await _authenticationService
                .Register(request.RegisterVM.Username, request.RegisterVM.Email, request.RegisterVM.Password);
        }
        catch ( Exception ex )
        {
            throw new Exception($"Registration exception occured: {ex.Message}", ex);
        }

        return token;
    }
}