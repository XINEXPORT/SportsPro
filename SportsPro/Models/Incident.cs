namespace SportsPro.Models
{
    public class Incident
    {
        public int IncidentID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DateOpened { get; set; } 

        public DateTime? DateClosed { get; set; }

        public int CustomerID { get; set; }

        public Customer? Customer { get; set; }

        public int ProductID { get; set; }

        public Product? Product { get; set; }

        public int? TechnicianID { get; set; }

        public Technician? Technician { get; set; }
    }
}
