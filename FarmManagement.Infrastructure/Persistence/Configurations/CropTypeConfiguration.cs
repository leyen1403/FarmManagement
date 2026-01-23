using FarmManagement.Domain.Entities.Crops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class CropTypeConfiguration : IEntityTypeConfiguration<CropType>
{
    public void Configure(EntityTypeBuilder<CropType> builder)
    {
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Code)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(ct => ct.Code)
               .IsUnique();

        builder.Property(ct => ct.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(ct => ct.Description)
               .HasMaxLength(255);

        builder.HasMany(ct => ct.Crops)
               .WithOne(c => c.CropType)
               .HasForeignKey(c => c.CropTypeId);

        // Soft delete
        builder.HasQueryFilter(ct => !ct.IsDeleted);
    }

}