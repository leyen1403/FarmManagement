using FarmManagement.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RoleCode).IsRequired().HasMaxLength(20);
        builder.Property(r => r.RoleName).IsRequired().HasMaxLength(50);

        builder.HasMany(r => r.UserRoles)
        .WithOne(ur => ur.Role)
        .HasForeignKey(ur => ur.RoleId);
    }
}