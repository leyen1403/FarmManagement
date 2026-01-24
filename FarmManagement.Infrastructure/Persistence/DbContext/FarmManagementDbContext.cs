using FarmManagement.Domain.Common;
using FarmManagement.Domain.Entities.Users;
using FarmManagement.Domain.Entities.Locations;
using FarmManagement.Domain.Entities.Crops;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FarmManagement.Infrastructure.Persistence.DbContext;

public class FarmManagementDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public FarmManagementDbContext(DbContextOptions<FarmManagementDbContext> options)
        : base(options)
    {
    }

    // Define DbSets for your entities here
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;

    public DbSet<LocationType> LocationTypes { get; set; } = null!;
    public DbSet<LocationStatus> LocationStatuses { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;

    public DbSet<CropType> CropTypes { get; set; } = null!;
    public DbSet<CropStatus> CropStatuses { get; set; } = null!;
    public DbSet<Crop> Crops { get; set; } = null!;
    public DbSet<CropHarvest> CropHarvests { get; set; } = null!;
    public DbSet<CostType> CostTypes { get; set; } = null!;
    public DbSet<CropCost> CropCosts { get; set; } = null!;
    public DbSet<CropCareType> CropCareTypes { get; set; } = null!;
    public DbSet<CropCareLog> CropCareLogs { get; set; } = null!;
    public DbSet<CropPrice> CropPrices { get; set; } = null!;

    // Common
    public DbSet<ActivityLog> ActivityLogs { get; set; } = null!;

    public DbSet<LivestockType> LivestockTypes { get; set; } = null!;
    public DbSet<LivestockStatus> LivestockStatuses { get; set; } = null!;
    public DbSet<Livestock> Livestocks { get; set; } = null!;
    public DbSet<LivestockHealthStatus> LivestockHealthStatuses { get; set; } = null!;
    public DbSet<LivestockHealthLog> LivestockHealthLogs { get; set; } = null!;
    public DbSet<LivestockCareType> LivestockCareTypes { get; set; } = null!;
    public DbSet<LivestockCareLog> LivestockCareLogs { get; set; } = null!;
    public DbSet<SaleType> SaleTypes { get; set; } = null!;
    public DbSet<LivestockSale> LivestockSales { get; set; } = null!;
    public DbSet<LivestockSaleDetail> LivestockSaleDetails { get; set; } = null!;

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                entry.Entity.CreatedDate = DateTime.UtcNow;
                entry.Entity.UpdatedDate = DateTime.UtcNow;
                entry.Entity.IsDeleted = false;
                break;

                case EntityState.Modified:
                entry.Entity.UpdatedDate = DateTime.UtcNow;
                break;

                case EntityState.Deleted:
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedDate = DateTime.UtcNow;
                break;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply global query filter for soft delete
        modelBuilder.Entity<Location>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<LocationType>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<LocationStatus>()
            .HasQueryFilter(x => !x.IsDeleted);

        // Apply all configurations using reflection
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<CropCost>(entity =>
        {
            entity.Property(e => e.Quantity).HasPrecision(18, 2);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<CropPrice>(entity =>
        {
            entity.Property(e => e.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Crop>(entity =>
        {
            entity.Property(e => e.EstimatedYield).HasPrecision(18, 2);
        });
    }
}