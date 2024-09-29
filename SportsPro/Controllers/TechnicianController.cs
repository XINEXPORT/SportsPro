using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;

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
        public IActionResult List()
        {
            var technicians = _context.Technicians
                .Where(t => t.TechnicianID != -1)
                .ToList();
            return View(technicians);
        }

        // GET THE ADD TECHNICIAN VIEW
        public IActionResult Add()
        {
            return View(new Technician());
        }

        // POST - ADD A TECHNICIAN
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
            return View(technician);
        }

        // POST - ADD THE EDITED TECHNICIAN
        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                _context.Technicians.Update(technician);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View(technician);
        }

        // GET THE TECHNICIAN YOU WANT TO DELETE
        public IActionResult Delete(int id)
        {
            var technician = _context.Technicians.Find(id);
            if (technician == null)
            {
                return NotFound();
            }
            return View(technician);
        }

        // POST DELETE THE TECHNICIAN
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
