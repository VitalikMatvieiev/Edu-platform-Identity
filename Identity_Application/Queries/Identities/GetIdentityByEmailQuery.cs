using AutoMapper;
using FluentValidation;
using Identity_Application.Interfaces.Repository;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Domain.Entities.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity_Application.Queries.Identities;

public record GetIdentityByEmailQuery(string Email) : IRequest<IdentityDTO>;

public class IdentityByEmailQueryValidator : AbstractValidator<GetIdentityByEmailQuery>
{
    public IdentityByEmailQueryValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Provided email is not correct");
    }
}

public class GetIdentityByEmailHandler : IRequestHandler<GetIdentityByEmailQuery, IdentityDTO>
{
    private const string includeProps = "ClaimIdentities.Claims,IdentityRole.Roles.ClaimRole.Claims";
    private readonly IGenericRepository<Identity> _identityRepository;
    private readonly IValidator<GetIdentityByEmailQuery> _validator;
    private readonly IMapper _mapper;

    public GetIdentityByEmailHandler(IGenericRepository<Identity> identityRepository, IMapper mapper)
    {
        _identityRepository = identityRepository;
        _mapper = mapper;
        _validator = new IdentityByEmailQueryValidator();
    }

    public async Task<IdentityDTO> Handle(GetIdentityByEmailQuery request, CancellationToken cancellationToken)
    {
        var errors = _validator.Validate(request);

        foreach (var error in errors.Errors)
        {
            throw new Exception(error.ErrorMessage);
        }

        var identities = await _identityRepository
            .GetAsync(i => i.Email == request.Email,
            includeProperties: includeProps);

        var identity = identities.FirstOrDefault();

        if (identity is null)
            throw new Exception($"Identity with given email: {request.Email} was not found");

        var result = _mapper
            .Map<IdentityDTO>(identity);

        return result;
    }
}