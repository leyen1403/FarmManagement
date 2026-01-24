using FarmManagement.Application.Interfaces.Crops;
using FarmManagement.Application.Interfaces.Locations;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Infrastructure.Persistence.DbContext;
using FarmManagement.Infrastructure.Services.Crops;
using FarmManagement.Infrastructure.Services.Locations;
using FarmManagement.Infrastructure.Services.Common;
using FarmManagement.Infrastructure.Services.Livestocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Register DbContext
            services.AddDbContext<FarmManagementDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Common Service implementations
            services.AddScoped<IActivityLogService, ActivityLogService>();

            // Register Location Service implementations
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILocationTypeService, LocationTypeService>();
            services.AddScoped<ILocationStatusService, LocationStatusService>();

            // Register Crop Service implementations
            services.AddScoped<ICropTypeService, CropTypeService>();
            services.AddScoped<ICropStatusService, CropStatusService>();
            services.AddScoped<ICropService, CropService>();
            services.AddScoped<ICropPriceService, CropPriceService>();
            services.AddScoped<ICropHarvestService, CropHarvestService>();
            services.AddScoped<ICropCostService, CropCostService>();
            services.AddScoped<ICropCareLogService, CropCareLogService>();
            services.AddScoped<ICostTypeService, CostTypeService>();
            services.AddScoped<ICropCareTypeService, CropCareTypeService>();

            // Livestock Service implementations
            services.AddScoped<ILivestockService, LivestockService>();
            services.AddScoped<ILivestockTypeService, LivestockTypeService>();
            services.AddScoped<ILivestockStatusService, LivestockStatusService>();
            services.AddScoped<ILivestockCareTypeService, LivestockCareTypeService>();
            services.AddScoped<ILivestockCareLogService, LivestockCareLogService>();
            services.AddScoped<ILivestockHealthStatusService, LivestockHealthStatusService>();
            services.AddScoped<ILivestockHealthLogService, LivestockHealthLogService>();
            services.AddScoped<ISaleTypeService, SaleTypeService>();
            services.AddScoped<ILivestockSaleService, LivestockSaleService>();
            services.AddScoped<ILivestockPriceService, LivestockPriceService>();

            return services;
        }
    }
}