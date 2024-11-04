using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models; 
using SportsPro.Models.ViewModels; 

namespace SportsPro.Controllers
{
    public class AddEditIncidentController : Controller
    {
        private readonly SportsProContext _context;

        // Constructor 
        public AddEditIncidentController(SportsProContext context)
        {
            _context = context;
        }

        // Edit
        public ViewResult Edit(int id)
        {
            var model = new AddEditIncidentViewModel
            {
                Customers = _context.Customers.ToList(),
                Products = _context.Products.ToList(),
                Technicians = _context.Technicians.ToList(),
                CurrentIncident = _context.Incidents.Find(id),
                OperationMode = "Edit" 
            };

            return View(model); 
        }

        // Add
        public ViewResult Add()
        {
     
            var model = new AddEditIncidentViewModel
            {
                Customers = _context.Customers.ToList(),
                Products = _context.Products.ToList(),
                Technicians = _context.Technicians.ToList(),
                CurrentIncident = new Incident(), 
                OperationMode = "Add" 
            };

            return View(model);
        }
    }
}
