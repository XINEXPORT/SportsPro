using System.Collections.Generic;
using SportsPro.Models;

namespace SportsPro.Models.ViewModels
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; } = null!;
        public Incident Incident { get; set; } = null!;
        public IEnumerable<Incident> Incidents { get; set; } = null!;
    }
}
