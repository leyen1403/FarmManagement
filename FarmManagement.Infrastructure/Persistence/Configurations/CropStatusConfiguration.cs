using FarmManagement.Domain.Entities.Crops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class CropStatusConfiguration : IEntityTypeConfiguration<CropStatus>
{
    public void Configure(EntityTypeBuilder<CropStatus> builder)
    {
        builder.HasKey(cs => cs.Id);
        builder.Property(cs => cs.Code).IsRequired().HasMaxLength(20);
        builder.Property(cs => cs.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(cs => cs.Crops)
               .WithOne(c => c.CropStatus)
               .HasForeignKey(c => c.CropStatusId);

        // Seed default crop statuses (created at DB initialization)
        builder.HasData(
            new CropStatus
            {
                Id = 1,
                Code = "PLANTED",
                Name = "Đang trồng",
                Description = "Cây đang trong giai đoạn sinh trưởng",
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new CropStatus
            {
                Id = 2,
                Code = "HARVESTED",
                Name = "Đã thu hoạch",
                Description = "Cây đã thu hoạch",
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new CropStatus
            {
                Id = 3,
                Code = "INACTIVE",
                Name = "Ngừng hoạt động",
                Description = "Trạng thái không còn sử dụng",
                IsActive = false,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        );
    }
}