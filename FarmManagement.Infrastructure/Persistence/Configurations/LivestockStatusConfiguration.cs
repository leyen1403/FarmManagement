using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockStatusConfiguration : IEntityTypeConfiguration<LivestockStatus>
{
    public void Configure(EntityTypeBuilder<LivestockStatus> builder)
    {
        builder.HasKey(ls => ls.Id);
        builder.Property(ls => ls.Code).IsRequired().HasMaxLength(20);
        builder.Property(ls => ls.Name).IsRequired().HasMaxLength(50);
    }
}