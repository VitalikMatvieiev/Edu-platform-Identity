using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity_Infrastructure.Configurations;

public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.Property(i => i.Id).IsRequired();
        builder.Property(i => i.Username).IsRequired();
        builder.Property(i => i.Email).IsRequired();
        builder.Property(i => i.Password).IsRequired();
        builder.Property(i => i.LastLogin);
        builder.HasMany(i => i.Claims).WithMany(c => c.Identities);
        builder.HasMany(i => i.Roles).WithMany(r => r.Identities);
        builder.HasOne(i => i.Token).WithOne(t => t.Identity);
    }
}