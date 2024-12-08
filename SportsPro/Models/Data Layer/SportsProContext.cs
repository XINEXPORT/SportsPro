using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models.DomainModels;

namespace SportsPro.Models
{
    public class SportsProContext : IdentityDbContext<User>
    {
        public SportsProContext(DbContextOptions<SportsProContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Technician> Technicians { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Incident> Incidents { get; set; } = null!;
        public DbSet<Registration> Registrations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Registration>().HasKey(r => new { r.CustomerID, r.ProductID });

            modelBuilder
                .Entity<Registration>()
                .Property(r => r.CustomerID)
                .HasColumnName("CustomerID");

            modelBuilder
                .Entity<Registration>()
                .Property(r => r.ProductID)
                .HasColumnName("ProductID");

            modelBuilder
                .Entity<Registration>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CustomerID);

            modelBuilder
                .Entity<Registration>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.ProductID);

            modelBuilder.ApplyConfiguration(new SeedProducts());
            modelBuilder.ApplyConfiguration(new SeedTechnicians());
            modelBuilder.ApplyConfiguration(new SeedCountries());
            modelBuilder.ApplyConfiguration(new SeedCustomers());
            modelBuilder.ApplyConfiguration(new SeedIncidents());
            modelBuilder.ApplyConfiguration(new SeedRegistrations());
        }
    }
}
