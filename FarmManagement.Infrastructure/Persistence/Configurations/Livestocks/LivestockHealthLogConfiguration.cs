using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockHealthLogConfiguration : IEntityTypeConfiguration<LivestockHealthLog>
{
    public void Configure(EntityTypeBuilder<LivestockHealthLog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CheckDate).IsRequired();
        builder.Property(x => x.Symptom).HasMaxLength(500);
        builder.Property(x => x.Treatment).HasMaxLength(500);
        builder.Property(x => x.MedicineCost).HasPrecision(18, 2);
        builder.Property(x => x.VetName).HasMaxLength(200);

        // Relationships
        builder.HasOne(x => x.Livestock)
          .WithMany(x => x.LivestockHealthLogs)
              .HasForeignKey(x => x.LivestockId)
      .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.HealthStatus)
         .WithMany(x => x.LivestockHealthLogs)
          .HasForeignKey(x => x.HealthStatusId)
   .OnDelete(DeleteBehavior.Restrict);
    }
}
