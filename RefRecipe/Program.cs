using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RefRecipe.Data;
using RefRecipe.Models;
using OfficeOpenXml;
using Glimpse.AspNet.Tab;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<RefRecipeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RefRecipeContext")));




var app = builder.Build();

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
