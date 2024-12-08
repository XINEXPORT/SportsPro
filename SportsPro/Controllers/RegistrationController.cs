using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Data.Configuration;
using SportsPro.Models;

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

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationViewModel
            {
                Customer = new Customer(),
                Customers = customerData.List(
                    new QueryOptions<Customer> { OrderBy = c => c.LastName }
                ),
            };

            return View(model);
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
        [Route("registration/registrations/{id?}")]
        public IActionResult List(int? id, [FromQuery] int? customerId)
        {
            int customerIdValue = id ?? customerId ?? 0;
            if (customerIdValue == 0)
            {
                TempData["message"] = "Customer ID is required.";
                return RedirectToAction("Index");
            }

            // Fetch customer with related registrations and products
            var customer = customerData.Get(
                new QueryOptions<Customer>
                {
                    Includes = "Registrations.Product",
                    Where = c => c.CustomerID == customerIdValue,
                }
            );

            if (customer == null)
            {
                TempData["message"] = "Customer not found. Please select a customer.";
                return RedirectToAction("Index");
            }

            // Fetch all available products
            var products = productData.List(new QueryOptions<Product> { OrderBy = p => p.Name });

            // Populate the RegistrationViewModel
            var model = new RegistrationViewModel { Customer = customer, Products = products };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (model.HasProduct)
            {
                var customer = customerData.Get(
                    new QueryOptions<Customer>
                    {
                        Includes = "Registrations.Product",
                        Where = c => c.CustomerID == model.Customer.CustomerID,
                    }
                );

                var product = productData.Get(model.Product.ProductID);

                if (customer == null || product == null)
                {
                    TempData["message"] = "Invalid customer or product.";
                    return RedirectToAction("Index");
                }

                if (customer.Registrations.Any(r => r.ProductID == product.ProductID))
                {
                    TempData["message"] =
                        $"{product.Name} is already registered to {customer.FullName}";
                }
                else
                {
                    customer.Registrations.Add(
                        new Registration
                        {
                            CustomerID = customer.CustomerID,
                            ProductID = product.ProductID,
                        }
                    );

                    customerData.Save();
                    TempData["message"] =
                        $"{product.Name} has been registered to {customer.FullName}";
                }
            }
            else
            {
                TempData["message"] = "You must select a product.";
            }

            return RedirectToAction("List", new { id = model.Customer.CustomerID });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(RegistrationViewModel model)
        {
            var customer = customerData.Get(
                new QueryOptions<Customer>
                {
                    Includes = "Registrations.Product",
                    Where = c => c.CustomerID == model.Customer.CustomerID,
                }
            );

            var product = productData.Get(model.Product.ProductID);

            if (customer == null || product == null)
            {
                TempData["message"] = "Invalid customer or product.";
                return RedirectToAction("Index");
            }

            var registration = customer.Registrations.FirstOrDefault(r =>
                r.ProductID == product.ProductID
            );

            if (registration != null)
            {
                customer.Registrations.Remove(registration);
                customerData.Save();
                TempData["message"] =
                    $"{product.Name} has been de-registered from {customer.FullName}";
            }
            else
            {
                TempData["message"] = "The product was not registered for this customer.";
            }

            return RedirectToAction("List", new { id = model.Customer.CustomerID });
        }
    }
}
