using FarmManagement.Domain.Entities.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class LivestockTypeConfiguration : IEntityTypeConfiguration<LivestockType>
{
    public void Configure(EntityTypeBuilder<LivestockType> builder)
    {
        builder.HasKey(lt => lt.Id);
        builder.Property(lt => lt.Code).IsRequired().HasMaxLength(20);
        builder.Property(lt => lt.Name).IsRequired().HasMaxLength(50);
        builder.Property(lt => lt.Description).HasMaxLength(255);
    }
}