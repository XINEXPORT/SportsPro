namespace SportsPro.Models.ViewModels
{
    public class IncidentManagerViewModel
    {
        public Incident Incident { get; set; } = null!;
        public string Action { get; set; } = string.Empty;

        public IEnumerable<Incident> Incidents { get; set; } = null!;
        public IEnumerable<Product> Products { get; set; } = null!;
        public IEnumerable<Technician> Technicians { get; set; } = null!;

        public string DisplayMode { get; set; } = "List";
    }
}
