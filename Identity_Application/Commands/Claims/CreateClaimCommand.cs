using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record CreateClaimCommand(ClaimVM ClaimVM) : IRequest<int>;

public class CreateClaimHandler : IRequestHandler<CreateClaimCommand, int>
{
    private readonly IGenericRepository<Claim> _claimRepository;
    private readonly IValidator<ClaimVM> _validator;

    public CreateClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
        _validator = new ClaimValidator();
    }

    public async Task<int> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        if (request.ClaimVM is null)
            throw new ArgumentNullException("Given data is not correct");

        var errors = _validator.Validate(request.ClaimVM);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        var claim = new Claim()
        {
            Name = request.ClaimVM.Name
        };

        int id = await _claimRepository.InsertAsync(claim);

        return id;
    }
}