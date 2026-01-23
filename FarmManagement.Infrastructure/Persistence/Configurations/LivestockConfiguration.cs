using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockConfiguration : IEntityTypeConfiguration<Livestock>
{
    public void Configure(EntityTypeBuilder<Livestock> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.TagCode).HasMaxLength(50);
        builder.Property(l => l.ImportDate).IsRequired();
        builder.Property(l => l.ImportWeight).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(l => l.ImportPrice).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(l => l.Note).HasMaxLength(255);

        builder.HasOne(l => l.LivestockType)
        .WithMany(lt => lt.Livestocks)
        .HasForeignKey(l => l.LivestockTypeId);

        builder.HasOne(l => l.LivestockStatus)
        .WithMany(ls => ls.Livestocks)
        .HasForeignKey(l => l.LivestockStatusId);
    }
}