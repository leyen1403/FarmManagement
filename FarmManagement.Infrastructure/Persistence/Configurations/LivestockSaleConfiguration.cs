using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockSaleConfiguration : IEntityTypeConfiguration<LivestockSale>
{
    public void Configure(EntityTypeBuilder<LivestockSale> builder)
    {
        builder.HasKey(ls => ls.Id);
        builder.Property(ls => ls.SaleDate).IsRequired();
        builder.Property(ls => ls.Weight).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(ls => ls.UnitPrice).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(ls => ls.TotalAmount).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(ls => ls.Buyer).HasMaxLength(100);
        builder.Property(ls => ls.Note).HasMaxLength(255);

        builder.HasOne(ls => ls.Livestock)
        .WithMany(l => l.LivestockSales)
        .HasForeignKey(ls => ls.LivestockId);

        builder.HasOne(ls => ls.SaleType)
        .WithMany(st => st.LivestockSales)
        .HasForeignKey(ls => ls.SaleTypeId);
    }
}