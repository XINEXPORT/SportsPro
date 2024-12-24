using System.Collections.Generic;
using SportsPro.Models;

namespace SportsPro.Models.ViewModels
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; } = new Technician();
        public Incident Incident { get; set; } = new Incident();
        public IEnumerable<Incident> Incidents { get; set; } = new List<Incident>();
    }
}
