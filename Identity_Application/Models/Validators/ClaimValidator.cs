using FluentValidation;
using Identity_Application.Models.BaseEntitiesModels;

namespace Identity_Application.Models.Validators;

public class ClaimValidator : AbstractValidator<ClaimVM>
{
    public ClaimValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Claim name is required")
            .MaximumLength(20).WithMessage("Claim name must be shorter than 20 symbols");
    }
}