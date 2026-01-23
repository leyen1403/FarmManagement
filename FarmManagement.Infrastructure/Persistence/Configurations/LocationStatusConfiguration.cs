using FarmManagement.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LocationStatusConfiguration : IEntityTypeConfiguration<LocationStatus>
{
    public void Configure(EntityTypeBuilder<LocationStatus> builder)
    {
        builder.HasKey(ls => ls.Id);
        builder.Property(ls => ls.Code).IsRequired().HasMaxLength(20);
        builder.Property(ls => ls.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(ls => ls.Locations)
        .WithOne(l => l.LocationStatus)
        .HasForeignKey(l => l.LocationStatusId);

        builder.HasData(
        new LocationStatus
        {
            Id = 1,
            Code = "ACTIVE",
            Name = "Đang hoạt động",
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        },
        new LocationStatus
        {
            Id = 2,
            Code = "INACTIVE",
            Name = "Ngừng hoạt động",
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        }
    );
    }
}