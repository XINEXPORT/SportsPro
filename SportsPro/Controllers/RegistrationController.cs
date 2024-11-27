using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using SportsPro.Data.Configuration;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private Repository<Customer> customerData { get; set; }
        private Repository<Product> productData { get; set; }
        public RegistrationController(SportsProContext ctx)
        {
            customerData = new Repository<Customer>(ctx);
            productData = new Repository<Product>(ctx);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationViewModel
            {
                Customer = new Customer(),
                Customers = customerData.List(new QueryOptions<Customer> { OrderBy = c => c.LastName })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(RegistrationViewModel model)
        {
            if (model.HasCustomer)
            {
                return RedirectToAction("List", new { id = model.Customer.CustomerID });
            }
            else
            {
                TempData["message"] = "You must select a customer.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("[controller]s/{id?}")]
        public IActionResult List(int id)
        {
            // Get selected customer and related products
            var options = new QueryOptions<Customer>
            {
                Includes = "Registrations.Product", // Load related registrations and products
                Where = c => c.CustomerID == id
            };

            var model = new RegistrationViewModel
            {
                Customer = customerData.Get(options)!,
                Products = productData.List(new QueryOptions<Product> { OrderBy = p => p.Name }) // Get products for dropdown
            };

            if (model.HasCustomer)
            {
                return View(model);
            }
            else
            {
                TempData["message"] = "Customer not found. Please select a customer.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (model.HasProduct)
            {
                // Get customer and product from database
                var customer = customerData.Get(new QueryOptions<Customer>
                {
                    Includes = "Registrations.Product",
                    Where = c => c.CustomerID == model.Customer.CustomerID
                });

                var product = productData.Get(model.Product.ProductID);

                if (customer == null || product == null)
                {
                    TempData["message"] = "Invalid customer or product.";
                    return RedirectToAction("Index");
                }

                // Check if product is already registered for this customer
                if (customer.Registrations.Any(r => r.ProductID == product.ProductID))
                {
                    TempData["message"] = $"{product.Name} is already registered to {customer.FullName}";
                }
                else
                {
                    // Add new registration
                    customer.Registrations.Add(new Registration
                    {
                        CustomerID = customer.CustomerID,
                        ProductID = product.ProductID
                    });

                    customerData.Save();
                    TempData["message"] = $"{product.Name} has been registered to {customer.FullName}";
                }
            }
            else
            {
                TempData["message"] = "You must select a product.";
            }

            return RedirectToAction("List", new { id = model.Customer.CustomerID });
        }

        [HttpPost]
        public IActionResult Delete(RegistrationViewModel model)
        {
            // Get customer and product from database
            var customer = customerData.Get(new QueryOptions<Customer>
            {
                Includes = "Registrations.Product",
                Where = c => c.CustomerID == model.Customer.CustomerID
            });

            var product = productData.Get(model.Product.ProductID);

            if (customer == null || product == null)
            {
                TempData["message"] = "Invalid customer or product.";
                return RedirectToAction("Index");
            }

            // Find the registration entry
            var registration = customer.Registrations.FirstOrDefault(r => r.ProductID == product.ProductID);

            if (registration != null)
            {
                // Remove the registration
                customer.Registrations.Remove(registration);
                customerData.Save();
                TempData["message"] = $"{product.Name} has been de-registered from {customer.FullName}";
            }
            else
            {
                TempData["message"] = "The product was not registered for this customer.";
            }

            return RedirectToAction("List", new { id = model.Customer.CustomerID });
        }
    }
}
