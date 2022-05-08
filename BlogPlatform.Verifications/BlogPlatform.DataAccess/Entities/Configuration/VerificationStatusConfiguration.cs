using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.DataAccess.Entities.Configuration
{
    internal class VerificationStatusConfiguration : BaseEntityConfiguration<VerificationStatus, int>
    {
        public override void Configure(EntityTypeBuilder<VerificationStatus> builder)
        {
            base.Configure(builder);

            builder.HasIndex(vs => vs.StatusName)
                   .IsUnique();

            builder.Property(vs => vs.StatusName)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
