using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Data.Configuration;
using SportsPro.Models;
using SportsPro.Models.DomainModels;

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

// Configure the DbContext
builder.Services.AddDbContext<SportsProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsPro"))
);

// **Add Identity Services**
builder
    .Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SportsProContext>()
    .AddDefaultTokenProviders();

// **Configure Identity Cookie Options**
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Redirect anonymous users to Login page
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect unauthorized users
});

// Build the app
var app = builder.Build();

// Seed admin and technician users and roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Create roles and users
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<User>>();

    await SeedUsersAndRoles(roleManager, userManager);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

// **Enable Authentication and Authorization Middleware**
app.UseAuthentication(); // Add this before Authorization
app.UseAuthorization();

// Route for /incident/listbytech
app.MapControllerRoute(
    name: "custom_route",
    pattern: "incident/listbytech/",
    defaults: new { controller = "TechIncident", action = "List" }
);

// Registration route
app.MapControllerRoute(
    name: "registrations_route",
    pattern: "registration/registrations/",
    defaults: new { controller = "Registration", action = "List" }
);

// Default routing
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the app
app.Run();

// Method to seed roles and users
async Task SeedUsersAndRoles(
    RoleManager<IdentityRole> roleManager,
    UserManager<User> userManager
)
{
    // Create Admin role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Create Technician role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("Technician"))
    {
        await roleManager.CreateAsync(new IdentityRole("Technician"));
    }

    // Seed Admin User
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        adminUser = new User
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(adminUser, "Admin@12345");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Seed Technician User
    var techUser = await userManager.FindByNameAsync("technician");
    if (techUser == null)
    {
        techUser = new User
        {
            UserName = "technician",
            Email = "technician@gmail.com",
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(techUser, "Tech@12345");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(techUser, "Technician");
        }
    }
}
