using FarmManagement.Web.Services.Crops;
using FarmManagement.Web.Services.Locations;
using FarmManagement.Web.Services.Common;
using FarmManagement.Web.Services.Livestocks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // <-- thay vì AddRazorPages()

// Add HttpClient for API
builder.Services.AddHttpClient("FarmApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});

// Register API clients - Locations
builder.Services.AddScoped<LocationTypeApiClient>();
builder.Services.AddScoped<LocationStatusApiClient>();
builder.Services.AddScoped<LocationApiClient>();

// Register API clients - Crops
builder.Services.AddScoped<CropTypeApiService>();
builder.Services.AddScoped<CropStatusApiService>();
builder.Services.AddScoped<CropApiService>();
builder.Services.AddScoped<CropPriceApiService>();
builder.Services.AddScoped<CropHarvestApiService>();
builder.Services.AddScoped<CropCostApiService>();
builder.Services.AddScoped<CropCareLogApiService>();
builder.Services.AddScoped<CostTypeApiService>();
builder.Services.AddScoped<CropCareTypeApiService>();

// Register API clients - Livestocks
builder.Services.AddScoped<LivestockApiService>();
builder.Services.AddScoped<LivestockTypeApiService>();
builder.Services.AddScoped<LivestockStatusApiService>();
builder.Services.AddScoped<LivestockCareTypeApiService>();
builder.Services.AddScoped<LivestockCareLogApiService>();
builder.Services.AddScoped<LivestockHealthStatusApiService>();
builder.Services.AddScoped<LivestockHealthLogApiService>();
builder.Services.AddScoped<SaleTypeApiService>();
builder.Services.AddScoped<LivestockSaleApiService>();
builder.Services.AddScoped<LivestockPriceApiService>();

// Register Export Services
builder.Services.AddScoped<FarmManagement.Application.Interfaces.Services.ILivestockPdfExportService, FarmManagement.Infrastructure.Services.Reports.LivestockPdfExportService>();
builder.Services.AddScoped<FarmManagement.Application.Interfaces.Services.ILivestockInvoiceExportService, FarmManagement.Infrastructure.Services.Reports.LivestockInvoiceExportService>();

// Register API clients - Common
builder.Services.AddScoped<ActivityLogApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
