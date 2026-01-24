using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockConfiguration : IEntityTypeConfiguration<Livestock>
{
    public void Configure(EntityTypeBuilder<Livestock> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TagCode).HasMaxLength(50);
        builder.Property(x => x.Name).HasMaxLength(200);
        builder.Property(x => x.Quantity).HasDefaultValue(1);
        builder.Property(x => x.MaleCount).HasDefaultValue(0);
        builder.Property(x => x.FemaleCount).HasDefaultValue(0);
        builder.Property(x => x.ImportWeight).HasPrecision(18, 2);
        builder.Property(x => x.TotalImportWeight).HasPrecision(18, 2);
        builder.Property(x => x.ImportPrice).HasPrecision(18, 2);
        builder.Property(x => x.TotalImportPrice).HasPrecision(18, 2);
        builder.Property(x => x.Note).HasMaxLength(1000);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // Relationships
        builder.HasOne(x => x.LivestockType)
            .WithMany(x => x.Livestocks)
            .HasForeignKey(x => x.LivestockTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.LivestockStatus)
            .WithMany(x => x.Livestocks)
            .HasForeignKey(x => x.LivestockStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
