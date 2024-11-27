using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Required for ICollection

namespace SportsPro.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product code.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Product code must be between 1 and 50 characters.")]
        public string ProductCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a product name.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name must be between 1 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(8,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Yearly Price must be greater than zero.")]
        public decimal YearlyPrice { get; set; }

        [Required(ErrorMessage = "Please enter a release date.")]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        // NAVIGATION PROPERTY FOR MANY-TO-MANY RELATIONSHIP WITH REGISTRATION
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
