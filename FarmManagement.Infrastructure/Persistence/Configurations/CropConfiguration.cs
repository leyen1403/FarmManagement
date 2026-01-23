using FarmManagement.Domain.Entities.Crops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class CropConfiguration : IEntityTypeConfiguration<Crop>
{
    public void Configure(EntityTypeBuilder<Crop> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.PlantDate).IsRequired();
        builder.Property(c => c.ExpectedHarvestDate);
        builder.Property(c => c.ActualHarvestDate);
        builder.Property(c => c.EstimatedYield).HasColumnType("decimal(10,2)");
        builder.Property(c => c.Unit).HasMaxLength(20);
        builder.Property(c => c.Note).HasMaxLength(255);

        builder.HasOne(c => c.CropType)
        .WithMany(ct => ct.Crops)
        .HasForeignKey(c => c.CropTypeId);

        builder.HasOne(c => c.CropStatus)
        .WithMany(cs => cs.Crops)
        .HasForeignKey(c => c.CropStatusId);

        builder.HasMany(c => c.CropHarvests)
        .WithOne(ch => ch.Crop)
        .HasForeignKey(ch => ch.CropId);

        builder.HasMany(c => c.CropCosts)
        .WithOne(cc => cc.Crop)
        .HasForeignKey(cc => cc.CropId);

        builder.HasMany(c => c.CropCareLogs)
        .WithOne(ccl => ccl.Crop)
        .HasForeignKey(ccl => ccl.CropId);
    }
}