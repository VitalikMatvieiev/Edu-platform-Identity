using AutoMapper;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;

namespace Identity_Application.Helpers.MappingConverters;

class RoleToRoleDTOConverter : ITypeConverter<Role, RoleDTO>
{
    public RoleDTO Convert(Role source, RoleDTO destination, ResolutionContext context)
    {
        var roleDto = new RoleDTO
        {
            Id = source.Id,
            Name = source.Name,
            Claims = new List<RoleClaimDTO>()
        };

        foreach (var claimRole in source.ClaimRole)
        {
            var claimDto = context.Mapper.Map<RoleClaimDTO>(claimRole);
            roleDto.Claims.Add(claimDto);
        }

        return roleDto;
    }
}