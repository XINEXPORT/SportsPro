using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.ViewModels;

namespace SportsPro.Controllers
{
    public class AddEditIncidentController : Controller
    {
        private SportsProContext context { get; set; }

        //CONSTRUCTOR
        public AddEditIncidentController(SportsProContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        // EDIT INCIDENT
        public ViewResult Edit(int id)
        {
            var model = new AddEditIncidentView();

            model.Products = context.Products.ToList();
            model.Technicians = context.Technicians.ToList();
            model.CurrentIncident = context.Incidents.Find(id);
            model.OperationMode = "Edit";

            return View(model);
        }

        //ADD INCIDENT
        public ViewResult Add()
        {
            var model = new AddEditIncidentView();

            model.Customers = context.Customers.ToList();
            model.Products = context.Products.ToList();
            model.Technicians = context.Technicians.ToList();
            model.CurrentIncident = new Incident(); 
            model.OperationMode = "Add";

            return View(model);
        }


    }
}
