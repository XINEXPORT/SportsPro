using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

public class SeedRegistrations : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.HasData(
            new Registration { CustomerID = 1002, ProductID = 1 },
            new Registration { CustomerID = 1002, ProductID = 3 },
            new Registration { CustomerID = 1010, ProductID = 2 },
            new Registration { CustomerID = 1010, ProductID = 4 },
            new Registration { CustomerID = 1004, ProductID = 5 },
            new Registration { CustomerID = 1006, ProductID = 6 },
            new Registration { CustomerID = 1008, ProductID = 7 }

        );
    }
}
