using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockSaleConfiguration : IEntityTypeConfiguration<LivestockSale>
{
    public void Configure(EntityTypeBuilder<LivestockSale> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderCode).HasMaxLength(50);
        builder.Property(x => x.SaleDate).IsRequired();
        builder.Property(x => x.TotalQuantity).HasDefaultValue(0);
        builder.Property(x => x.TotalWeight).HasPrecision(18, 2).HasDefaultValue(0);
        builder.Property(x => x.TotalAmount).HasPrecision(18, 2).HasDefaultValue(0);
        builder.Property(x => x.Buyer).HasMaxLength(200);
        builder.Property(x => x.BuyerPhone).HasMaxLength(20);
        builder.Property(x => x.Note).HasMaxLength(1000);
        builder.Property(x => x.Status).IsRequired().HasDefaultValue(OrderStatus.Completed);
        builder.Property(x => x.CreatedDate).IsRequired();

        // Relationships
        builder.HasOne(x => x.Livestock)
            .WithMany(x => x.LivestockSales)
            .HasForeignKey(x => x.LivestockId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.SaleType)
            .WithMany(x => x.LivestockSales)
            .HasForeignKey(x => x.SaleTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.OrderCode).IsUnique();
        builder.HasIndex(x => x.LivestockId);
        builder.HasIndex(x => x.SaleDate);
    }
}
