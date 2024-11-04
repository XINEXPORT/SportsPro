using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models.ViewModels;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class IncidentManagerController : Controller
    {
        private readonly SportsProContext _context;

        // Constructor
        public IncidentManagerController(SportsProContext context)
        {
            _context = context;
        }

        public ViewResult Index(string mode)
        {
            var model = new IncidentManagerViewModel
            {
                Incidents = GetIncidentsByMode(mode),
                DisplayMode = mode
            };
            return View(model);
        }

        private List<Incident> GetIncidentsByMode(string mode)
        {
            if (mode == "Unassigned")
            {
                return _context.Incidents.Where(i => i.TechnicianID == null).ToList();
            }
            else if (mode == "Open")
            {
                return _context.Incidents.Where(i => i.DateClosed == null).ToList();
            }
            else
            {
                return _context.Incidents.ToList(); 
            }
        }
    }
}
