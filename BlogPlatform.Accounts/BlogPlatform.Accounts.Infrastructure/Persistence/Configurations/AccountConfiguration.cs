using BlogPlatform.Verifications.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Verifications.Infrastructure.Persistence.Configurations;

internal class AccountConfiguration : BaseEntityConfiguration<Account>
{
    public override void Configure(EntityTypeBuilder<Account> builder)
    {
        base.Configure(builder);
        
        builder.OwnsOne(ac => ac.Name, entity =>
        {
            entity.Property(name => name.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(name => name.MiddleName)
                .HasMaxLength(50);

            entity.Property(name => name.LastName)
                .IsRequired()
                .HasMaxLength(50);
        })
        .Navigation(ac => ac.Name)
        .IsRequired();

        builder.OwnsOne(ac => ac.Location, entity =>
        {
            entity.Property(loc => loc.City)
                .HasMaxLength(50);

            entity.Property(loc => loc.State)
                .HasMaxLength(50);

            entity.Property(loc => loc.Country)
                .IsRequired()
                .HasMaxLength(50);
        })
        .Navigation(ac => ac.Location)
        .IsRequired();

        builder.Property(ac => ac.AvatarPath)
            .HasMaxLength(500);
    }
}
