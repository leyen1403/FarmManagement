using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockCareLogConfiguration : IEntityTypeConfiguration<LivestockCareLog>
{
    public void Configure(EntityTypeBuilder<LivestockCareLog> builder)
    {
        builder.HasKey(lcl => lcl.Id);
        builder.Property(lcl => lcl.CareDate).IsRequired();
        builder.Property(lcl => lcl.Quantity).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(lcl => lcl.Unit).HasMaxLength(20);
        builder.Property(lcl => lcl.Cost).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(lcl => lcl.Note).HasMaxLength(255);

        builder.HasOne(lcl => lcl.Livestock)
        .WithMany(l => l.LivestockCareLogs)
        .HasForeignKey(lcl => lcl.LivestockId);

        builder.HasOne(lcl => lcl.LivestockCareType)
        .WithMany(lct => lct.LivestockCareLogs)
        .HasForeignKey(lcl => lcl.LivestockCareTypeId);
    }
}