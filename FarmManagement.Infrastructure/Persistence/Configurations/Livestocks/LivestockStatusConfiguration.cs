using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockStatusConfiguration : IEntityTypeConfiguration<LivestockStatus>
{
    public void Configure(EntityTypeBuilder<LivestockStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
  new LivestockStatus
  {
      Id = 1,
      Code = "RAISING",
      Name = "Đang nuôi",
      CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
      UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
  },
       new LivestockStatus
       {
           Id = 2,
           Code = "SOLD",
           Name = "Đã bán",
           CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
           UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
       },
          new LivestockStatus
          {
              Id = 3,
              Code = "BREEDING",
              Name = "Đang sinh sản",
              CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
              UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
          },
          new LivestockStatus
          {
              Id = 4,
              Code = "SICK",
              Name = "Đang bệnh",
              CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
              UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
          },
            new LivestockStatus
            {
                Id = 5,
                Code = "QUARANTINE",
                Name = "Cách ly",
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
       new LivestockStatus
       {
           Id = 6,
           Code = "DEAD",
           Name = "Đã chết",
           CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
           UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
       },
          new LivestockStatus
          {
              Id = 7,
              Code = "SLAUGHTERED",
              Name = "Đã giết mổ",
              CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
              UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
          }
        );
    }
}
