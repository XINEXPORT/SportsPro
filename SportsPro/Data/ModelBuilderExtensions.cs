using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasData(
                    new Product
                    {
                        ProductID = 1,
                        ProductCode = "DRAFT10",
                        Name = "Draft Manager 1.0",
                        YearlyPrice = 4.99M,
                        ReleaseDate = DateTime.Parse("2017-02-01"),
                    },
                    new Product
                    {
                        ProductID = 2,
                        ProductCode = "DRAFT20",
                        Name = "Draft Manager 2.0",
                        YearlyPrice = 5.99M,
                        ReleaseDate = DateTime.Parse("2019-07-15 00:00:00.000"),
                    },
                    new Product
                    {
                        ProductID = 3,
                        ProductCode = "LEAG10",
                        Name = "League Scheduler 1.0",
                        YearlyPrice = 4.99M,
                        ReleaseDate = DateTime.Parse("2016-05-01 00:00:00.000"),
                    },
                    new Product
                    {
                        ProductID = 4,
                        ProductCode = "LEAGD10",
                        Name = "League Scheduler Deluxe 1.0",
                        YearlyPrice = 7.99M,
                        ReleaseDate = DateTime.Parse("2016-08-01 00:00:00.000"),
                    },
                    new Product
                    {
                        ProductID = 5,
                        ProductCode = "TEAM10",
                        Name = "Team Manager 1.0",
                        YearlyPrice = 4.99M,
                        ReleaseDate = DateTime.Parse("2017-05-01 00:00:00.000"),
                    },
                    new Product
                    {
                        ProductID = 6,
                        ProductCode = "TRNY10",
                        Name = "Tournament Master 1.0",
                        YearlyPrice = 4.99M,
                        ReleaseDate = DateTime.Parse("2015-12-01 00:00:00.000"),
                    },
                    new Product
                    {
                        ProductID = 7,
                        ProductCode = "TRNY20",
                        Name = "Tournament Master 2.0",
                        YearlyPrice = 5.99M,
                        ReleaseDate = DateTime.Parse("2018-02-15 00:00:00.000"),
                    }
                );

            modelBuilder
                .Entity<Technician>()
                .HasData(
                    new Technician
                    {
                        TechnicianID = -1,
                        Name = "Not assigned",
                        Email = "",
                        Phone = "",
                    },
                    new Technician
                    {
                        TechnicianID = 11,
                        Name = "Alison Diaz",
                        Email = "alison@sportsprosoftware.com",
                        Phone = "800-555-0443",
                    },
                    new Technician
                    {
                        TechnicianID = 12,
                        Name = "Jason Lee",
                        Email = "jason@sportsprosoftware.com",
                        Phone = "800-555-0444",
                    },
                    new Technician
                    {
                        TechnicianID = 13,
                        Name = "Andrew Wilson",
                        Email = "awilson@sportsprosoftware.com",
                        Phone = "800-555-0449",
                    },
                    new Technician
                    {
                        TechnicianID = 14,
                        Name = "Gunter Wendt",
                        Email = "gunter@sportsprosoftware.com",
                        Phone = "800-555-0400",
                    },
                    new Technician
                    {
                        TechnicianID = 15,
                        Name = "Gina Fiori",
                        Email = "gfiori@sportsprosoftware.com",
                        Phone = "800-555-0459",
                    }
                );

            modelBuilder
                .Entity<Country>()
                .HasData(
                    new Country { CountryID = "AU", Name = "Australia" },
                    new Country { CountryID = "AT", Name = "Austria" },
                    new Country { CountryID = "BE", Name = "Belgium" },
                    new Country { CountryID = "BR", Name = "Brazil" },
                    new Country { CountryID = "CA", Name = "Canada" },
                    new Country { CountryID = "CN", Name = "China" },
                    new Country { CountryID = "DK", Name = "Denmark" },
                    new Country { CountryID = "FI", Name = "Finland" },
                    new Country { CountryID = "FR", Name = "France" },
                    new Country { CountryID = "GR", Name = "Greece" },
                    new Country { CountryID = "US", Name = "United States" },
                    new Country { CountryID = "VN", Name = "Vietnam" }
                );

            modelBuilder
                .Entity<Customer>()
                .HasData(
                    new Customer
                    {
                        CustomerID = 1002,
                        FirstName = "Ania",
                        LastName = "Irvin",
                        Address = "PO Box 96621",
                        City = "Washington",
                        State = "DC",
                        PostalCode = "20090",
                        CountryID = "US",
                        Phone = "(301) 555-8950",
                        Email = "ania@mma.nidc.com",
                    },
                    new Customer
                    {
                        CustomerID = 1004,
                        FirstName = "Kenzie",
                        LastName = "Quinn",
                        Address = "1990 Westwood Blvd Ste 260",
                        City = "Los Angeles",
                        State = "CA",
                        PostalCode = "90025",
                        CountryID = "US",
                        Phone = "(800) 555-8725",
                        Email = "kenzie@mma.jobtrak.com",
                    }
                );

            modelBuilder
                .Entity<Incident>()
                .HasData(
                    new Incident
                    {
                        IncidentID = 1,
                        CustomerID = 1002,
                        ProductID = 1,
                        TechnicianID = 11,
                        Title = "Could not install",
                        Description = "Media appears to be bad.",
                        DateOpened = DateTime.Parse("2020-01-08"),
                        DateClosed = DateTime.Parse("2020-01-10"),
                    },
                    new Incident
                    {
                        IncidentID = 2,
                        CustomerID = 1002,
                        ProductID = 4,
                        TechnicianID = 14,
                        Title = "Error importing data",
                        Description = "Received error message 415 while trying to import data from previous version.",
                        DateOpened = DateTime.Parse("2020-01-09"),
                        DateClosed = null,
                    }
                );
        }
    }
}
