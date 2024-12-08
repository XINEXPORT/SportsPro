using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly SportsProContext _context;

        // Constructor
        public TechnicianController(SportsProContext context)
        {
            _context = context;
        }

        // GET THE TECHNICIAN LIST
        [Authorize]
        [Route("technicians")]
        public IActionResult List()
        {
            var technicians = _context.Technicians.Where(t => t.TechnicianID != -1).ToList();
            return View(technicians);
        }

        // GET THE ADD TECHNICIAN VIEW
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View("AddEdit", new Technician());
        }

        // POST - ADD A TECHNICIAN
        [Authorize]
        [HttpPost]
        public IActionResult Add(Technician technician)
        {
            if (ModelState.IsValid)
            {
                _context.Technicians.Add(technician);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View(technician);
        }

        // GET THE EDIT TECHNICIAN VIEW
        public IActionResult Edit(int id)
        {
            var technician = _context.Technicians.Find(id);
            if (technician == null)
            {
                return NotFound();
            }
            return View("AddEdit", technician);
        }

        // POST - ADD THE EDITED TECHNICIAN
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                _context.Technicians.Update(technician);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View("AddEdit", technician);
        }

        // GET THE TECHNICIAN YOU WANT TO DELETE
        public IActionResult Delete(int id)
        {
            var technician = _context.Technicians.Find(id);
            if (technician == null)
            {
                return NotFound();
            }
            return View("Delete", technician);
        }

        // POST DELETE THE TECHNICIAN
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var technician = _context.Technicians.Find(id);
            if (technician == null)
            {
                return NotFound();
            }

            _context.Technicians.Remove(technician);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
