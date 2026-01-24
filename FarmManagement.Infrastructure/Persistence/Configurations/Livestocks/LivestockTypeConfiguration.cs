using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockTypeConfiguration : IEntityTypeConfiguration<LivestockType>
{
    public void Configure(EntityTypeBuilder<LivestockType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
        new LivestockType
        {
            Id = 1,
            Code = "PIG",
            Name = "Heo",
            Description = "Heo thịt, heo nái, heo giống",
            CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },
     new LivestockType
     {
         Id = 2,
         Code = "COW",
         Name = "Bò",
         Description = "Bò thịt, bò sữa, bò giống",
         CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
         UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
     },
          new LivestockType
          {
              Id = 3,
              Code = "CHICKEN",
              Name = "Gà",
              Description = "Gà thịt, gà đẻ, gà giống",
              CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
              UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
          },
   new LivestockType
   {
       Id = 4,
       Code = "DUCK",
       Name = "Vịt",
       Description = "Vịt thịt, vịt đẻ",
       CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
       UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
   },
            new LivestockType
            {
                Id = 5,
                Code = "GOAT",
                Name = "Dê",
                Description = "Dê thịt, dê sữa",
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new LivestockType
            {
                Id = 6,
                Code = "SHEEP",
                Name = "Cừu",
                Description = "Cừu lấy lông, cừu thịt",
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
       new LivestockType
       {
           Id = 7,
           Code = "FISH",
           Name = "Cá",
           Description = "Cá nuôi ao, hồ, bè",
           CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
           UpdatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
       }
 );
    }
}
