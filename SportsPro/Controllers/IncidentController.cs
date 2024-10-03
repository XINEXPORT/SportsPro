using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public IncidentController(SportsProContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");
        // GET THE INCIDENT LIST
        public IActionResult List()
        {
            List<Incident> incidents = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .OrderBy(i => i.DateOpened)
                .ToList();
            return View(incidents);
        }

        // GET THE ADD INCIDENT VIEW
        public void StoreDataInViewBag(string action)
        {
            //this a dynamic object that allows u to pass data from the controller to the view
            ViewBag.Action = action;
            ViewBag.Customers = context.Customers
                .OrderBy(c => c.FirstName)
                .ToList();
            ViewBag.Products = context.Products
                .OrderBy(c => c.Name)
                .ToList();
            ViewBag.Technicians = context.Technicians
                .OrderBy(c => c.Name)
                .ToList();
        }

        // GET ADD - NEW INCIDENT
        [HttpGet]
        public IActionResult Add()
        {
            StoreDataInViewBag("Add");
            return View("AddEdit", new Incident());
        }
        
        //GET EDIT - FETCH THE INCIDENT ID FOR EDITING
        [HttpGet]

        public IActionResult Edit(int id)
        {
            StoreDataInViewBag("Edit");
            var product = context.Incidents.Find(id);
            return View("AddEdit");
        }
        //POST & SAVE
        [HttpPost]

        public IActionResult Save(Incident incident) 
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
                if (incident.IncidentID == 0)
                {
                    StoreDataInViewBag("Add");
                }
                else 
                {
                    StoreDataInViewBag("Edit");
                }
                return View("AddEdit", incident);
            }
        }

        // GET DELETE RECORD
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = context.Incidents.Find(id);
            return View(product);
        }

        //POST DELETED RECORD
        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            return RedirectToAction("List");
            
        }




    

    }
}