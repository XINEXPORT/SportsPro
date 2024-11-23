using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.Data;
using SportsPro.Models.ViewModels;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private const string TECH_KEY = "techID";

        private readonly IRepository<Technician> _technicianRepo;
        private readonly IRepository<Incident> _incidentRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TechIncidentController(
            IRepository<Technician> technicianRepo,
            IRepository<Incident> incidentRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _technicianRepo = technicianRepo;
            _incidentRepo = incidentRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Technicians = _technicianRepo.GetAll().OrderBy(t => t.Name).ToList();

            var technician = new Technician();
            var session = _httpContextAccessor.HttpContext?.Session;
            int? techID = session?.GetInt32(TECH_KEY);

            if (techID.HasValue)
            {
                technician = _technicianRepo.Get(techID.Value);
            }

            return View(technician);
        }

        [HttpPost]
        public IActionResult List(Technician technician)
        {
            if (technician.TechnicianID == 0)
            {
                TempData["message"] = "You must select a technician.";
                return RedirectToAction("Index");
            }
            else
            {
                var session = _httpContextAccessor.HttpContext?.Session;
                session?.SetInt32(TECH_KEY, technician.TechnicianID);
                return RedirectToAction("List", new { id = technician.TechnicianID });
            }
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            var technician = _technicianRepo.Get(id);
            if (technician == null)
            {
                return RedirectToAction("Index");
            }

            var model = new TechIncidentViewModel
            {
                Technician = technician,
                Incidents = _incidentRepo.GetAll()
                    .Include(i => i.Customer)
                    .Include(i => i.Product)
                    .Where(i => i.TechnicianID == id && i.DateClosed == null)
                    .OrderBy(i => i.DateOpened)
                    .ToList(),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var session = _httpContextAccessor.HttpContext?.Session;

            if (session == null)
            {
                TempData["message"] = "Session is not available. Please select a technician.";
                return RedirectToAction("Index");
            }

            int? techID = session.GetInt32(TECH_KEY);

            if (!techID.HasValue)
            {
                TempData["message"] = "Technician not found. Please select a technician.";
                return RedirectToAction("Index");
            }

            var technician = _technicianRepo.Get(techID.Value);
            if (technician == null)
            {
                TempData["message"] = "Technician not found. Please select a technician.";
                return RedirectToAction("Index");
            }

            // Retrieve the incident with related data
            var incident = _incidentRepo.GetAll()
                .Include(i => i.Customer) 
                .Include(i => i.Product)  
                .FirstOrDefault(i => i.IncidentID == id);

            if (incident == null)
            {
                TempData["message"] = "Incident not found.";
                return RedirectToAction("Index");
            }

            // Prepare the ViewModel
            var model = new TechIncidentViewModel
            {
                Technician = technician,
                Incident = incident,
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            var incident = _incidentRepo.Get(model.Incident.IncidentID);
            if (incident != null)
            {
                incident.Description = model.Incident.Description;
                incident.DateClosed = model.Incident.DateClosed;

                _incidentRepo.Update(incident);
                _incidentRepo.SaveChanges();
            }

            var session = _httpContextAccessor.HttpContext?.Session;
            int? techID = session?.GetInt32(TECH_KEY);
            return RedirectToAction("List", new { id = techID });
        }
    }
}
