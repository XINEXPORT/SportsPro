namespace SportsPro.Models
{
    public class Registration
    {
        public int CustomerID { get; set; } // Foreign key for Customer
        public Customer Customer { get; set; } = null!;

        public int ProductID { get; set; } // Foreign key for Product
        public Product Product { get; set; } = null!;
    }
}
