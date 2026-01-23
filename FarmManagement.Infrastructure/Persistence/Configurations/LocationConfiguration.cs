using FarmManagement.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
        builder.Property(l => l.Area).HasColumnType("decimal(10,2)");
        builder.Property(l => l.Capacity).HasColumnType("decimal(10,2)");
        builder.Property(l => l.CapacityUnit).HasMaxLength(20);
        builder.Property(l => l.Note).HasMaxLength(255);

        builder.HasOne(l => l.LocationType)
        .WithMany(lt => lt.Locations)
        .HasForeignKey(l => l.LocationTypeId);

        builder.HasOne(l => l.LocationStatus)
        .WithMany(ls => ls.Locations)
        .HasForeignKey(l => l.LocationStatusId);

        builder.HasOne(l => l.ParentLocation)
        .WithMany(l => l.ChildLocations)
        .HasForeignKey(l => l.ParentLocationId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}