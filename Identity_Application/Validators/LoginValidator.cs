using FluentValidation;
using Identity_Application.Models.AuthorizationModels;

namespace Identity_Application.Validators;

public class LoginValidator : AbstractValidator<LoginVM>
{
    private static readonly string _emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    public LoginValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Email is required")
            .Matches(_emailRegex).WithMessage("Email is not correct")
            .MaximumLength(40).WithMessage("Email must be shorter than 40 symbols");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Password must have min 5 symbols length");
    }
}