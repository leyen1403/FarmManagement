using FarmManagement.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ActionType)
        .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.EntityType)
       .IsRequired()
 .HasMaxLength(50);

        builder.Property(a => a.EntityName)
   .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Description)
         .IsRequired()
     .HasMaxLength(500);

        builder.Property(a => a.PerformedBy)
                .HasMaxLength(100);

        // Index for quick queries
        builder.HasIndex(a => a.ActionDate);
        builder.HasIndex(a => new { a.EntityType, a.EntityId });
    }
}
