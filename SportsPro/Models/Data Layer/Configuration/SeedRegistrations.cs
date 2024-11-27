using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SportsPro.Models
{
    internal class SeedRegistrations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            // seed Customer/Product many-to-many linking table
            entity
                .HasMany(c => c.Products)
                .WithMany(p => p.Customers)
                .UsingEntity(
                cp => cp.HasData(
                    new { CustomersCustomerID = 1002, ProductsProductID = 1 },
                    new { CustomersCustomerID = 1002, ProductsProductID = 3 },
                    new { CustomersCustomerID = 1010, ProductsProductID = 2 }
                ));
        }
    }
}