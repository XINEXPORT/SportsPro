using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private readonly SportsProContext _context;

        // Constructor
        public CustomerController(SportsProContext context)
        {
            _context = context;
        }

        // GET THE CUSTOMER LIST

        [Route("customers")]
        public IActionResult List()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        // GET THE ADD CUSTOMER VIEW
        public IActionResult Add()
        {
            return View(new Customer());
        }

        // POST -  ADD A CUSTOMER
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View(customer);
        }

        // GET THE EDIT CUSTOMER VIEW
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST - ADD THE EDITED CUSTOMER
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View(customer);
        }

        // GET THE DELETE CUSTOMER VIEW
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST - DELETE THE CUSTOMER
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
