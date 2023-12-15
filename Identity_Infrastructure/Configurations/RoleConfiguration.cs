using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity_Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(i => i.Id).IsRequired().UseIdentityColumn();
        builder.Property(r => r.Name).IsRequired().HasMaxLength(20);

        builder.HasMany(r => r.IdentityRole)
            .WithOne(ir => ir.Roles).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(r => r.ClaimRole)
            .WithOne(cr => cr.Roles).OnDelete(DeleteBehavior.NoAction);
    }
}