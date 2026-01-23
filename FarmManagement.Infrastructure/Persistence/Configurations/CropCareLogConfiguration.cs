using FarmManagement.Domain.Entities.Crops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class CropCareLogConfiguration : IEntityTypeConfiguration<CropCareLog>
{
    public void Configure(EntityTypeBuilder<CropCareLog> builder)
    {
        builder.HasKey(ccl => ccl.Id);
        builder.Property(ccl => ccl.CareDate).IsRequired();
        builder.Property(ccl => ccl.Description).HasMaxLength(255);
        builder.Property(ccl => ccl.Cost).HasColumnType("decimal(10,2)").IsRequired();

        builder.HasOne(ccl => ccl.Crop)
        .WithMany(c => c.CropCareLogs)
        .HasForeignKey(ccl => ccl.CropId);

        builder.HasOne(ccl => ccl.CropCareType)
        .WithMany(cct => cct.CropCareLogs)
        .HasForeignKey(ccl => ccl.CropCareTypeId);
    }
}