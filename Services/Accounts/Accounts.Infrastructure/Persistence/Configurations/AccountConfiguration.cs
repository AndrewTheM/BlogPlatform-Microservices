using Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.Configurations;

internal class AccountConfiguration : BaseEntityConfiguration<Account>
{
    public override void Configure(EntityTypeBuilder<Account> builder)
    {
        base.Configure(builder);

        builder.HasIndex(ac => ac.UserId)
            .IsUnique();

        builder.OwnsOne(ac => ac.Name, entity =>
        {
            entity.Property(name => name.FirstName)
                .HasMaxLength(50);

            entity.Property(name => name.MiddleName)
                .HasMaxLength(50);

            entity.Property(name => name.LastName)
                .HasMaxLength(50);
        });

        builder.OwnsOne(ac => ac.Location, entity =>
        {
            entity.Property(loc => loc.City)
                .HasMaxLength(50);

            entity.Property(loc => loc.State)
                .HasMaxLength(50);

            entity.Property(loc => loc.Country)
                .HasMaxLength(50);
        });

        builder.Property(ac => ac.AvatarPath)
            .HasMaxLength(500);
    }
}
