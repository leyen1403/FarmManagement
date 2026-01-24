using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockSaleDetailConfiguration : IEntityTypeConfiguration<LivestockSaleDetail>
{
    public void Configure(EntityTypeBuilder<LivestockSaleDetail> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.Weight).HasPrecision(18, 2).HasDefaultValue(0);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Note).HasMaxLength(500);
        builder.Property(x => x.LineNumber).IsRequired().HasDefaultValue(1);

        // Relationship
        builder.HasOne(x => x.LivestockSale)
      .WithMany(x => x.Details)
            .HasForeignKey(x => x.LivestockSaleId)
     .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(x => new { x.LivestockSaleId, x.LineNumber });
    }
}
