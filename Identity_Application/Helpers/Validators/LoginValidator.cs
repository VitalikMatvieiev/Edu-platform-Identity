using FluentValidation;
using Identity_Application.Models.AuthorizationModels;

namespace Identity_Application.Helpers.Validators;

public class LoginValidator : AbstractValidator<LoginVM>
{
    public LoginValidator()
    {
        RuleFor(l => l.Username)
            .MaximumLength(20).WithMessage("Username must be shorter than 20 symbols");

        RuleFor(l => l.Email)
            .EmailAddress().WithMessage("Email is not correct")
            .MaximumLength(40).WithMessage("Email must be shorter than 40 symbols");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Password must have min 5 symbols length");
    }
}