using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity_Infrastructure.Configurations;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.Property(c => c.Id).IsRequired().UseIdentityColumn();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(20);

        builder.HasMany(c => c.ClaimIdentity)
            .WithOne(ci => ci.Claims)
            .HasForeignKey(ci => ci.ClaimsId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.ClaimRole)
            .WithOne(cr => cr.Claims)
            .HasForeignKey(cr => cr.ClaimsId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(IdentityDbContext.SeedData<Claim>("ClaimsSeed"));
    }
}