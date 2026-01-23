using FarmManagement.Domain.Entities.Crops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class CropHarvestConfiguration : IEntityTypeConfiguration<CropHarvest>
{
    public void Configure(EntityTypeBuilder<CropHarvest> builder)
    {
        builder.HasKey(ch => ch.Id);
        builder.Property(ch => ch.HarvestDate).IsRequired();
        builder.Property(ch => ch.Quantity).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(ch => ch.Unit).HasMaxLength(20);
        builder.Property(ch => ch.UnitPrice).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(ch => ch.TotalAmount).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(ch => ch.Buyer).HasMaxLength(100);
        builder.Property(ch => ch.Note).HasMaxLength(255);

        builder.HasOne(ch => ch.Crop)
        .WithMany(c => c.CropHarvests)
        .HasForeignKey(ch => ch.CropId);
    }
}