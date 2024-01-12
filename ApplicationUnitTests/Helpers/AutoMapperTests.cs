using AutoMapper;
using Identity_Application.Helpers;
using Identity_Application.Models.BaseEntitiesDTOs;
using Identity_Domain.Entities.Base;

namespace ApplicationUnitTests.Helpers;

public class AutoMapperTests
{
    private readonly IMapper _mapper;

    public AutoMapperTests()
    {
        var mappingConfig = 
            new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

        _mapper = mappingConfig.CreateMapper();
    }

    [Fact]
    public void Should_Map_Identity_To_IdentityDTO()
    {
        // Arrange
        var identity = new Identity
        {
            Id = 1,
            Username = "Test",
            Email = "Test@gmail.com",
            PasswordSalt = "ojuhdgfp9wuhefpquf",
            PasswordHash = "asodaofsaofq24r3tgwg3"
        };

        // Act
        var identityDto = _mapper.Map<IdentityDTO>(identity);

        // Assert
        Assert.Equal(identity.Id, identityDto.Id);
        Assert.Equal(identity.Username, identityDto.Username);
        Assert.Equal(identity.Email, identityDto.Email);
        Assert.Equal(identity.PasswordSalt, identityDto.PasswordSalt);
        Assert.Equal(identity.PasswordHash, identityDto.PasswordHash);
    }

    [Fact]
    public void Should_Map_Role_To_RoleDTO()
    {
        // Arrange
        var role = new Role
        {
            Id = 1,
            Name = "test"
        };

        // Act
        var roleDto = _mapper.Map<RoleDTO>(role);

        // Assert
        Assert.Equal(role.Id, roleDto.Id);
        Assert.Equal(role.Name, roleDto.Name);
    }

    [Fact]
    public void Should_Map_Claim_To_ClaimDTO()
    {
        // Arrange
        var claim = new Claim
        {
            Id = 1,
            Name = "Test"
        };

        // Act
        var claimDto = _mapper.Map<ClaimDTO>(claim);

        // Assert
        Assert.Equal(claim.Id, claimDto.Id);
        Assert.Equal(claim.Name, claimDto.Name);
    }
}