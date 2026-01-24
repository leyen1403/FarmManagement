using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockCareLogConfiguration : IEntityTypeConfiguration<LivestockCareLog>
{
    public void Configure(EntityTypeBuilder<LivestockCareLog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CareDate).IsRequired();
        builder.Property(x => x.Quantity).HasPrecision(18, 2);
        builder.Property(x => x.Unit).HasMaxLength(50);
        builder.Property(x => x.Cost).HasPrecision(18, 2);
        builder.Property(x => x.Note).HasMaxLength(1000);

        // Relationships
        builder.HasOne(x => x.Livestock)
            .WithMany(x => x.LivestockCareLogs)
            .HasForeignKey(x => x.LivestockId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.LivestockCareType)
            .WithMany(x => x.LivestockCareLogs)
            .HasForeignKey(x => x.LivestockCareTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
