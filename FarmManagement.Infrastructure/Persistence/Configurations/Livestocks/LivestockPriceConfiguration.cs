using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Persistence.Configurations.Livestocks;

public class LivestockPriceConfiguration : IEntityTypeConfiguration<LivestockPrice>
{
    public void Configure(EntityTypeBuilder<LivestockPrice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.EffectiveFrom).IsRequired();
        builder.Property(x => x.EffectiveTo);
        builder.Property(x => x.Note).HasMaxLength(500);
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.HasOne(x => x.Livestock)
        .WithMany(x => x.LivestockPrices)
        .HasForeignKey(x => x.LivestockId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.LivestockId, x.Gender, x.EffectiveFrom });
    }
}
