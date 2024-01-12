using FluentValidation;
using Identity_Application.Models.AuthorizationModels;

namespace Identity_Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterVM>
{
    private static readonly string _emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    public RegisterValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(20).WithMessage("Username must be shorter than 20 symbols");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .Matches(_emailRegex).WithMessage("Email is not correct")
            .MaximumLength(40).WithMessage("Email must be shorter than 40 symbols");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Password must have min 5 symbols length");
    }
}