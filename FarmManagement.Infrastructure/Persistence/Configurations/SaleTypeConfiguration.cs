using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class SaleTypeConfiguration : IEntityTypeConfiguration<SaleType>
{
    public void Configure(EntityTypeBuilder<SaleType> builder)
    {
        builder.HasKey(st => st.Id);
        builder.Property(st => st.Code).IsRequired().HasMaxLength(20);
        builder.Property(st => st.Name).IsRequired().HasMaxLength(50);
    }
}