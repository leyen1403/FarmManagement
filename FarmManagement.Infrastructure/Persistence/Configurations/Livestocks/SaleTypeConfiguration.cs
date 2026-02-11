using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class SaleTypeConfiguration : IEntityTypeConfiguration<SaleType>
{
    public void Configure(EntityTypeBuilder<SaleType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SaleMethod).IsRequired().HasDefaultValue(SaleMethod.PerKilogram);

        // Seed Data với SaleMethod phù hợp
        builder.HasData(
            new SaleType { Id = 1, Code = "PER_HEAD", Name = "Bán theo con", SaleMethod = SaleMethod.PerHead },
            new SaleType { Id = 2, Code = "PER_KG", Name = "Bán theo kg", SaleMethod = SaleMethod.PerKilogram },
            new SaleType { Id = 3, Code = "PER_LOT", Name = "Bán theo lô/đàn", SaleMethod = SaleMethod.PerLot },
            new SaleType { Id = 4, Code = "PER_SET", Name = "Bán theo bộ/cặp", SaleMethod = SaleMethod.PerSet }
        );
    }
}
