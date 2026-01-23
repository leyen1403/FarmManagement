using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockCareTypeConfiguration : IEntityTypeConfiguration<LivestockCareType>
{
    public void Configure(EntityTypeBuilder<LivestockCareType> builder)
    {
        builder.HasKey(lct => lct.Id);
        builder.Property(lct => lct.Code).IsRequired().HasMaxLength(20);
        builder.Property(lct => lct.Name).IsRequired().HasMaxLength(50);
    }
}