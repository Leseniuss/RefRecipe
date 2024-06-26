using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RefRecipe.Data;
using RefRecipe.Models;
using OfficeOpenXml;
using Serilog;
using Serilog.Events;
using RefRecipe;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<RefRecipeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RefRecipeContext")));

// Add ExcelReader service
builder.Services.AddScoped<ExcelReader>();


var app = builder.Build();

Log.Logger = new LoggerConfiguration()
            //.MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("C:\\reseptit\\Log\\Logger.txt", rollingInterval: RollingInterval.Day)
            // Ota k�ytt��n Warning-tason logit
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            // Ota k�ytt��n Information-tason logit
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .CreateLogger();
Log.Information("Ohjelma k�ynnistetty");

// seed the db
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
