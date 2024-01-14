using FluentValidation;
using Identity_Application.Helpers.Validators;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;

namespace Identity_Application.Commands.Claims;

public record UpdateClaimCommand(int Id, ClaimVM ClaimVM) : IRequest;

public class UpdateClaimHandler : IRequestHandler<UpdateClaimCommand>
{
    private readonly IGenericRepository<Claim> _claimRepository;
    private readonly IValidator<ClaimVM> _validator;

    public UpdateClaimHandler(IGenericRepository<Claim> claimRepository)
    {
        _claimRepository = claimRepository;
        _validator = new ClaimValidator();
    }

    public async Task Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
    {
        if (request.ClaimVM is null)
            throw new ArgumentNullException("Given data is not correct");

        var errors = _validator.Validate(request.ClaimVM);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        var claims = await _claimRepository.GetAsync(c => c.Id == request.Id);
        var claim = claims.FirstOrDefault();

        if (claim is null)
            throw new Exception("Claim with given id was not found.");

        claim.Name = request.ClaimVM.Name;

        await _claimRepository.UpdateAsync(claim);
    }
}