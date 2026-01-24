using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockHealthStatusConfiguration : IEntityTypeConfiguration<LivestockHealthStatus>
{
    public void Configure(EntityTypeBuilder<LivestockHealthStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        // Seed Data
        builder.HasData(
            new LivestockHealthStatus { Id = 1, Code = "HEALTHY", Name = "Khỏe mạnh" },
     new LivestockHealthStatus { Id = 2, Code = "SICK", Name = "Ốm/Bệnh" },
    new LivestockHealthStatus { Id = 3, Code = "RECOVERING", Name = "Đang hồi phục" },
     new LivestockHealthStatus { Id = 4, Code = "WEAK", Name = "Yếu" },
       new LivestockHealthStatus { Id = 5, Code = "PREGNANT", Name = "Mang thai" },
   new LivestockHealthStatus { Id = 6, Code = "NURSING", Name = "Đang cho con bú" },
            new LivestockHealthStatus { Id = 7, Code = "INJURED", Name = "Bị thương" }
        );
    }
}
