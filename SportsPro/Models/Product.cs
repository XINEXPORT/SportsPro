using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
		public int ProductID { get; set; }
		public string ProductCode { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		[Column(TypeName = "decimal(8,2)")]

        [Range(0.01, double.MaxValue, ErrorMessage = "Yearly Price must be greater than zero.")]
        public decimal YearlyPrice { get; set; }
		public DateTime ReleaseDate { get; set; } = DateTime.Now;
	}
}
