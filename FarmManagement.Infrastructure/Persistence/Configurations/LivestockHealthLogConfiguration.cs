using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockHealthLogConfiguration : IEntityTypeConfiguration<LivestockHealthLog>
{
    public void Configure(EntityTypeBuilder<LivestockHealthLog> builder)
    {
        builder.HasKey(lhl => lhl.Id);
        builder.Property(lhl => lhl.CheckDate).IsRequired();
        builder.Property(lhl => lhl.Symptom).HasMaxLength(255);
        builder.Property(lhl => lhl.Treatment).HasMaxLength(255);
        builder.Property(lhl => lhl.MedicineCost).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(lhl => lhl.VetName).HasMaxLength(100);

        builder.HasOne(lhl => lhl.Livestock)
        .WithMany(l => l.LivestockHealthLogs)
        .HasForeignKey(lhl => lhl.LivestockId);

        builder.HasOne(lhl => lhl.HealthStatus)
        .WithMany(hs => hs.LivestockHealthLogs)
        .HasForeignKey(lhl => lhl.HealthStatusId);
    }
}