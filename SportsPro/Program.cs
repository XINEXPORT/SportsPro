using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.Data; // Include the namespace for IRepository and Repository

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

// Configure the DbContext
builder.Services.AddDbContext<SportsProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsPro"))
);

// Register the repository for dependency injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Add this line

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

// Route for /incident/listbytech
app.MapControllerRoute(
    name: "custom_route",
    pattern: "incident/listbytech/",
    defaults: new { controller = "TechIncident", action = "List" }
);

// Default routing
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
