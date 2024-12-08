using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private readonly SportsProContext context;

        public IncidentController(SportsProContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        // GET: /incidents
        [Authorize]
        [Route("incidents")]
        public IActionResult List(string filter = "All")
        {
            ViewData["Filter"] = filter;

            // List all Incidents
            IQueryable<Incident> incidents = context
                .Incidents.Include(i => i.Customer)
                .Include(i => i.Product)
                .OrderBy(i => i.DateOpened);

            // Apply filtering
            switch (filter)
            {
                case "Unassigned":
                    incidents = incidents.Where(i => i.TechnicianID == -1);
                    break;
                case "Open":
                    incidents = incidents.Where(i => i.DateClosed == null);
                    break;
                case "All":
                default:
                    break;
            }

            return View(incidents.ToList());
        }

        // Store data in ViewBag for Add/Edit views
        public void StoreDataInViewBag(string action)
        {
            ViewBag.Action = action;
            ViewBag.Customers = context.Customers.OrderBy(c => c.FirstName).ToList();
            ViewBag.Products = context.Products.OrderBy(p => p.Name).ToList();
            ViewBag.Technicians = context.Technicians.OrderBy(t => t.Name).ToList();
        }

        // GET: Add - New Incident
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            StoreDataInViewBag("Add");
            return View("AddEdit", new SportsPro.Models.Incident());
        }

        // GET: Edit - Fetch Incident by ID for editing
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            StoreDataInViewBag("Edit");
            var incident = context.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }
            return View("AddEdit", incident);
        }

        // POST: Save Incident
        [Authorize]
        [HttpPost]
        public IActionResult Save(SportsPro.Models.Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                {
                    context.Incidents.Add(incident);
                }
                else
                {
                    context.Incidents.Update(incident);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                StoreDataInViewBag(incident.IncidentID == 0 ? "Add" : "Edit");
                return View("AddEdit", incident);
            }
        }

        // GET: Confirm Delete Incident by ID
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = context.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }
            return View(incident);
        }

        // POST: Delete Incident
        [Authorize]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var incident = context.Incidents.Find(id);
            if (incident != null)
            {
                context.Incidents.Remove(incident);
                context.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
