using Microsoft.EntityFrameworkCore;
using SportsPro.Data; // Add this to reference IRepository and Repository
using SportsPro.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Dependency Injection (DI)
builder.Services.AddTransient<IRepository<Technician>, TechnicianRepository>();
builder.Services.AddTransient<IRepository<Incident>, IncidentRepository>();

// Register the generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add services to the container
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SportsProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsPro"))
);

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
