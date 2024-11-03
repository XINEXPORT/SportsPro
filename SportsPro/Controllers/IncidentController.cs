using System.Linq;
using System.Threading.Tasks;
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
        [Route("incidents")]
        public IActionResult List()
        {
            List<SportsPro.Models.Incident> incidents = context
                .Incidents.Include(i => i.Customer)
                .Include(i => i.Product)
                .OrderBy(i => i.DateOpened)
                .ToList();
            return View(incidents);
        }

        // Store data in ViewBag for Add/Edit views
        public void StoreDataInViewBag(string action)
        {
            ViewBag.Action = action;
            ViewBag.Customers = context.Customers.OrderBy(c => c.FirstName).ToList();
            ViewBag.Products = context.Products.OrderBy(p => p.Name).ToList();
            ViewBag.Technicians = context.Technicians.OrderBy(t => t.Name).ToList();
        }

        // GET -  Add - New Incident
        [HttpGet]
        public IActionResult Add()
        {
            StoreDataInViewBag("Add");
            return View("AddEdit", new SportsPro.Models.Incident());
        }

        // GET - Edit - Fetch Incident by ID for editing
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
