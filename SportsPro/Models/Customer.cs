using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Required for ICollection

namespace SportsPro.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        // FIRST NAME
        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(
            50,
            MinimumLength = 1,
            ErrorMessage = "First name must be between 1 and 50 characters."
        )]
        public string FirstName { get; set; } = string.Empty;

        // LAST NAME
        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(
            50,
            MinimumLength = 1,
            ErrorMessage = "Last name must be between 1 and 50 characters."
        )]
        public string LastName { get; set; } = string.Empty;

        // ADDRESS
        [Required(ErrorMessage = "Please enter an address.")]
        [StringLength(
            50,
            MinimumLength = 1,
            ErrorMessage = "Address must be between 1 and 50 characters."
        )]
        public string Address { get; set; } = string.Empty;

        // CITY
        [Required(ErrorMessage = "Please enter a city.")]
        [StringLength(
            50,
            MinimumLength = 1,
            ErrorMessage = "City must be between 1 and 50 characters."
        )]
        public string City { get; set; } = string.Empty;

        // STATE
        [Required(ErrorMessage = "Please enter a state.")]
        [StringLength(
            50,
            MinimumLength = 1,
            ErrorMessage = "State must be between 1 and 50 characters."
        )]
        public string State { get; set; } = string.Empty;

        // ZIP CODE
        [Required(ErrorMessage = "Please enter a postal code.")]
        [StringLength(
            20,
            MinimumLength = 1,
            ErrorMessage = "Postal code must be between 1 and 20 characters."
        )]
        public string PostalCode { get; set; } = string.Empty;

        // PHONE
        [StringLength(20, ErrorMessage = "Phone number must be less than 21 characters.")]
        [RegularExpression(
            @"\(\d{3}\) \d{3}-\d{4}",
            ErrorMessage = "Phone number must be in the format (999) 999-9999"
        )]
        public string? Phone { get; set; }

        // EMAIL
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(
            50,
            MinimumLength = 1,
            ErrorMessage = "Email must be between 1 and 50 characters."
        )]
        public string? Email { get; set; }

        // COUNTRY
        [Required(ErrorMessage = "Please select a country.")]
        public string CountryID { get; set; } = string.Empty;
        public Country? Country { get; set; }

        // FULL NAME
        public string FullName => $"{FirstName} {LastName}";

        // NAVIGATION PROPERTY FOR MANY-TO-MANY RELATIONSHIP WITH REGISTRATION
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
