using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    [Table("Registrations")] 
    public class Registration
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!; 

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!; 
    }
}
