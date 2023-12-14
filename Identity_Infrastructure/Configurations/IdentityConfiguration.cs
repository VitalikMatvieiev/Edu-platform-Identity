using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity_Infrastructure.Configurations;

public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.Property(i => i.Id).IsRequired().UseIdentityColumn();
        builder.Property(i => i.Username).IsRequired().HasMaxLength(20);
        builder.Property(i => i.Email).IsRequired().HasMaxLength(40);
        builder.Property(i => i.PasswordSalt).IsRequired();
        builder.Property(i => i.PasswordHash).IsRequired();
        builder.Property(i => i.LastLogin);
        builder.HasMany(i => i.Claims).WithMany(c => c.Identities);
        builder.HasMany(i => i.Roles).WithMany(r => r.Identities);
        builder.HasOne(i => i.Token).WithOne(t => t.Identity);
    }
}