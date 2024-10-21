using SportsPro.Models;
using System.Collections.Generic;

namespace SportsPro.ViewModels
{
    public class AddEditIncidentView
    {
        public List<Customer>? Customers { get; set; }

        public List<Product>? Products { get; set; }

        public List<Technician>? Technicians { get; set; }

        public Incident? CurrentIncident { get; set; }

        public string? OperationMode { get; set; }
    }
}
