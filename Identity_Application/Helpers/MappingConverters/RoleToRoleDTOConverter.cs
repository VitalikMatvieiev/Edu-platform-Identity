using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;

namespace Identity_Application.Helpers.MappingConverters;

class RoleToRoleDTOConverter : ITypeConverter<Role, RoleDTO>
{
    public RoleDTO Convert(Role source, RoleDTO destination, ResolutionContext context)
    {
        RoleDTO roleDto = WriteDataFromRole(source);

        WriteClaimsFromRole(ref roleDto, source, context);

        return roleDto;
    }

    private RoleDTO WriteDataFromRole(Role source)
    {
        var roleDto = new RoleDTO
        {
            Id = source.Id,
            Name = source.Name,
            Claims = new List<RoleClaimDTO>()
        };

        return roleDto;
    }

    private void WriteClaimsFromRole(ref RoleDTO roleDto, Role source, ResolutionContext context)
    {
        foreach (var claimRole in source.ClaimRole)
        {
            var claimDto = context.Mapper.Map<RoleClaimDTO>(claimRole);
            roleDto.Claims.Add(claimDto);
        }
    }
}