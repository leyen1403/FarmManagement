using FarmManagement.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LocationTypeConfiguration : IEntityTypeConfiguration<LocationType>
{
    public void Configure(EntityTypeBuilder<LocationType> builder)
    {
        builder.HasKey(lt => lt.Id);
        builder.Property(lt => lt.Code).IsRequired().HasMaxLength(20);
        builder.Property(lt => lt.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(lt => lt.Locations)
        .WithOne(l => l.LocationType)
        .HasForeignKey(l => l.LocationTypeId);

        builder.HasData(
        new LocationType
        {
            Id = 1,
            Code = "FARM",
            Name = "Nông trại",
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        },
        new LocationType
        {
            Id = 2,
            Code = "WAREHOUSE",
            Name = "Kho",
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        }
    );
    }
}