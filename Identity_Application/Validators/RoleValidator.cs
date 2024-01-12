using FluentValidation;
using Identity_Application.Models.BaseEntitiesModels;

namespace Identity_Application.Validators;

public class RoleValidator : AbstractValidator<RoleVM>
{
    public RoleValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Role name is required")
            .MaximumLength(20).WithMessage("Role name must be shorter than 20 symbols");
    }
}