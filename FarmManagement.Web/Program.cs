using FarmManagement.Web.Services.Crops;
using FarmManagement.Web.Services.Locations;
using FarmManagement.Web.Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // <-- thay vì AddRazorPages()

// Add HttpClient for API
builder.Services.AddHttpClient("FarmApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});

// Register API clients
builder.Services.AddScoped<LocationTypeApiClient>();
builder.Services.AddScoped<LocationStatusApiClient>();
builder.Services.AddScoped<LocationApiClient>();
builder.Services.AddScoped<CropTypeApiService>();
builder.Services.AddScoped<CropStatusApiService>();
builder.Services.AddScoped<CropApiService>();
builder.Services.AddScoped<CropPriceApiService>();
builder.Services.AddScoped<CropHarvestApiService>();
builder.Services.AddScoped<CropCostApiService>();
builder.Services.AddScoped<CropCareLogApiService>();
builder.Services.AddScoped<CostTypeApiService>();
builder.Services.AddScoped<CropCareTypeApiService>();
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
