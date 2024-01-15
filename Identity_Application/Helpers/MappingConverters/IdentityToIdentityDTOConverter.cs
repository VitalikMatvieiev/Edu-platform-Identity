using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;

namespace Identity_Application.Helpers.MappingConverters;

class IdentityToIdentityDTOConverter : ITypeConverter<Identity, IdentityDTO>
{
    public IdentityDTO Convert(Identity source, IdentityDTO destination, ResolutionContext context)
    {
        var identityDto = WriteDataFromIdentity(source);

        WriteRolesFromIdentity(ref identityDto, source, context);

        WriteClaimsFromIdentity(ref identityDto, source, context);

        return identityDto;
    }

    private IdentityDTO WriteDataFromIdentity(Identity source)
    {
        var identityDto = new IdentityDTO
        {
            Id = source.Id,
            Username = source.Username,
            Email = source.Email,
            PasswordSalt = source.PasswordSalt,
            PasswordHash = source.PasswordHash,
            RegistrationDate = source.RegistrationDate,
            LastLogin = source.LastLogin,
            LastLogout = source.LastLogout,
            Token = source.Token != null ? source.Token.Token : null,
            Roles = new List<IdentityRoleDTO>(),
            Claims = new List<IdentityClaimDTO>()
        };

        return identityDto;
    }

    private void WriteRolesFromIdentity(ref IdentityDTO identityDto, Identity source, ResolutionContext context)
    {
        foreach (var identityRole in source.IdentityRole)
        {
            var roleDto = context.Mapper.Map<IdentityRoleDTO>(identityRole);
            identityDto.Roles.Add(roleDto);
        }
    }

    private void WriteClaimsFromIdentity(ref IdentityDTO identityDto, Identity source, ResolutionContext context)
    {
        foreach (var claimIdentity in source.ClaimIdentities)
        {
            var claimDto = context.Mapper.Map<IdentityClaimDTO>(claimIdentity);
            identityDto.Claims.Add(claimDto);
        }
    }
}