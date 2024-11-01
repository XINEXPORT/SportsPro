namespace SportsPro.Models
{
    public class Incident
    {
        public int IncidentID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DateOpened { get; set; } // Ensure this is defined only once

        public DateTime? DateClosed { get; set; }

        public int CustomerID { get; set; }

        public Customer Customer { get; set; } = null!;

        public int ProductID { get; set; }

        public Product Product { get; set; } = null!;

        public int? TechnicianID { get; set; }

        public Technician Technician { get; set; } = null!;
    }
}
