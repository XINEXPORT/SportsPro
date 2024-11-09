using System.Collections.Generic;
using SportsPro.Models;

namespace SportsPro.Models.ViewModels
{
    public class AddEditIncidentViewModel
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Product> Products { get; set; } = new List<Product>();

        public List<Technician> Technicians { get; set; } = new List<Technician>();

        public Incident CurrentIncident { get; set; } = new Incident();

        public string OperationMode { get; set; } = string.Empty;
    }
}
