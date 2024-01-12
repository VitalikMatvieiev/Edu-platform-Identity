using AutoMapper;
using Identity_Application.Helpers.MappingConverters;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Additional;
using Identity_Domain.Entities.Base;

namespace Identity_Application.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Claim, ClaimDTO>()
            .ForMember(c => c.Id, o => o
                .MapFrom(c => c.Id))
            .ForMember(c => c.Name, o => o
                .MapFrom(c => c.Name));

        CreateMap<ClaimRole, RoleClaimDTO>()
            .ForMember(r => r.Id, o => o
                .MapFrom(r => r.Claims.Id))
            .ForMember(r => r.Name, o => o
                .MapFrom(r => r.Claims.Name));

        CreateMap<Role, RoleDTO>()
            .ConvertUsing<RoleToRoleDTOConverter>();

        CreateMap<IdentityRole, IdentityRoleDTO>()
            .ForMember(ir => ir.Id, o => o
                .MapFrom(ir => ir.Roles.Id))
            .ForMember(ir => ir.Name, o => o
                .MapFrom(ir => ir.Roles.Name));

        CreateMap<ClaimIdentity, IdentityClaimDTO>()
            .ForMember(ic => ic.Id, o => o
                .MapFrom(src => src.Claims.Id))
            .ForMember(ic => ic.Name, o => o
                .MapFrom(src => src.Claims.Name));

        CreateMap<Identity, IdentityDTO>()
            .ConvertUsing<IdentityToIdentityDTOConverter>();
    }
}