using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;

namespace Identity_Application.Helpers.MappingConverters;

class IdentityToIdentityDTOConverter : ITypeConverter<Identity, IdentityDTO>
{
    public IdentityDTO Convert(Identity source, IdentityDTO destination, ResolutionContext context)
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

        foreach (var identityRole in source.IdentityRole)
        {
            var roleDto = context.Mapper.Map<IdentityRoleDTO>(identityRole);
            identityDto.Roles.Add(roleDto);
        }

        foreach (var claimIdentity in source.ClaimIdentities)
        {
            var claimDto = context.Mapper.Map<IdentityClaimDTO>(claimIdentity);
            identityDto.Claims.Add(claimDto);
        }

        return identityDto;
    }
}