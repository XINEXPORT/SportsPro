using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;


namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private SportsProContext context { get; set; }

        // Constructor
        public CustomerController(SportsProContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        // GET THE CUSTOMER LIST

        [Route("customers")]
        public IActionResult List()
        {
            List<Customer> customers = context
                .Customers
                .OrderBy(i => i.FullName)
                .ToList();
            return View(customers);
        }

        // GET THE ADD CUSTOMER VIEW
        public void StoreDataInViewBag(string action)
        {
            ViewBag.Action = action;
            ViewBag.Customers = context.Customers.OrderBy(c => c.FullName).ToList();
        }

        //GET ADD - ADD NEW CUSTOMER
        [HttpGet]
        public IActionResult Add()
        {
            StoreDataInViewBag("Add");
            return View("AddEdit", new Customer());
        }


        // GET EDIT - FETCH THE EDIT ID FOR EDITING
        [HttpGet]
        public IActionResult Edit(int id)
        {
            StoreDataInViewBag("Edit");
            var customer = context.Customers.Find(id);
            return View("AddEdit", customer);
        }


        // POST & SAVE
        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    context.Customers.Add(customer);
                }
                else
                {
                    context.Customers.Update(customer);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                if (customer.CustomerID == 0)
                {
                    StoreDataInViewBag("Add");
                }
                else
                {
                    StoreDataInViewBag("Edit");
                }
                return View("AddEdit", customer);
            }
        }

        // GET THE DELETE CUSTOMER VIEW
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        // POST - DELETE THE CUSTOMER
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
