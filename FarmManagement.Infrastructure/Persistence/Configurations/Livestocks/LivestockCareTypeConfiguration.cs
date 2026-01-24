using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockCareTypeConfiguration : IEntityTypeConfiguration<LivestockCareType>
{
    public void Configure(EntityTypeBuilder<LivestockCareType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        // Seed Data
        builder.HasData(
   new LivestockCareType { Id = 1, Code = "FEED", Name = "Cho ăn" },
            new LivestockCareType { Id = 2, Code = "VACCINE", Name = "Tiêm vaccine" },
new LivestockCareType { Id = 3, Code = "MEDICINE", Name = "Cho uống thuốc" },
  new LivestockCareType { Id = 4, Code = "CLEAN", Name = "Vệ sinh" },
 new LivestockCareType { Id = 5, Code = "WEIGH", Name = "Cân trọng lượng" },
    new LivestockCareType { Id = 6, Code = "CHECKUP", Name = "Khám sức khỏe" },
  new LivestockCareType { Id = 7, Code = "DEWORMING", Name = "Tẩy giun" },
        new LivestockCareType { Id = 8, Code = "MOVE", Name = "Di chuyển" }
);
    }
}
