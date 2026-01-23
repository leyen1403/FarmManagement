using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockHealthStatusConfiguration : IEntityTypeConfiguration<LivestockHealthStatus>
{
    public void Configure(EntityTypeBuilder<LivestockHealthStatus> builder)
    {
        builder.HasKey(lhs => lhs.Id);
        builder.Property(lhs => lhs.Code).IsRequired().HasMaxLength(20);
        builder.Property(lhs => lhs.Name).IsRequired().HasMaxLength(50);
    }
}