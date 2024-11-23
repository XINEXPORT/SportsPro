using Microsoft.AspNetCore.Mvc;
using SportsPro.Data;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        // Example: Validate if a Technician exists in the database
        [HttpGet]
        public IActionResult ValidateTechnician([FromServices] IRepository<Technician> technicianRepo, int id)
        {
            var technician = technicianRepo.Get(id);
            if (technician == null)
            {
                return Json($"Technician with ID {id} does not exist.");
            }

            return Json(true); 
        }

        // Example: Validate if an Incident exists in the database
        [HttpGet]
        public IActionResult ValidateIncident([FromServices] IRepository<Incident> incidentRepo, int id)
        {
            var incident = incidentRepo.Get(id);
            if (incident == null)
            {
                return Json($"Incident with ID {id} does not exist.");
            }

            return Json(true); 
        }

        // Example: Validate unique email for Technician (Client-side validation)
        [HttpGet]
        public IActionResult ValidateUniqueTechnicianEmail([FromServices] IRepository<Technician> technicianRepo, string email)
        {
            var technicianExists = technicianRepo.GetAll().Any(t => t.Email == email);
            if (technicianExists)
            {
                return Json($"Email {email} is already in use.");
            }

            return Json(true);
        }
    }
}
