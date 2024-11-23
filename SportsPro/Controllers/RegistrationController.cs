using Microsoft.AspNetCore.Mvc;
using SportsPro.Models; 
using SportsPro.Data; 
using System.Linq;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Registration> _registrationRepository;

        public RegistrationController(
            IRepository<Customer> customerRepository,
            IRepository<Product> productRepository,
            IRepository<Registration> registrationRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _registrationRepository = registrationRepository;
        }

        // Display customers for selection
        public IActionResult GetCustomer()
        {
            var customers = _customerRepository.GetAll();
            if (!customers.Any())
            {
                ViewBag.ErrorMessage = "No customers found.";
            }

            return View(customers);
        }

        // Display registrations for a selected customer
        public IActionResult List()
        {
            var registrations = _registrationRepository
                .GetAll()
                .Select(r => new
                {
                    Customer = _customerRepository.GetById(r.CustomerId),
                    Product = _productRepository.GetById(r.ProductId)
                })
                .ToList();

            return View(registrations); 
        }
        public IActionResult Registrations(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Customer not found.";
                return RedirectToAction("GetCustomer");
            }

            var registrations = _registrationRepository
                .GetAll()
                .Where(r => r.CustomerId == customerId)
                .Select(r => r.Product)
                .ToList();

            ViewBag.Customer = customer;
            ViewBag.Products = _productRepository.GetAll();

            if (!registrations.Any())
            {
                ViewBag.Message = "No products registered for this customer.";
            }

            return View(registrations);
        }

        // Register a product for a customer
        [HttpPost]
        public IActionResult RegisterProduct(int customerId, int productId)
        {
            // Validate customer and product existence
            var customer = _customerRepository.GetById(customerId);
            var product = _productRepository.GetById(productId);

            if (customer == null || product == null)
            {
                TempData["ErrorMessage"] = "Invalid customer or product.";
                return RedirectToAction("Registrations", new { customerId });
            }

            // Check for existing registration
            var existingRegistration = _registrationRepository
                .GetAll()
                .FirstOrDefault(r => r.CustomerId == customerId && r.ProductId == productId);

            if (existingRegistration != null)
            {
                TempData["ErrorMessage"] = "This product is already registered for the customer.";
            }
            else
            {
                var newRegistration = new Registration
                {
                    CustomerId = customerId,
                    ProductId = productId
                };

                _registrationRepository.Add(newRegistration);
                _registrationRepository.Save();
                TempData["SuccessMessage"] = "Product registered successfully.";
            }

            return RedirectToAction("Registrations", new { customerId });
        }

        // Unregister a product for a customer
        [HttpPost]
        public IActionResult UnregisterProduct(int customerId, int productId)
        {
            var registration = _registrationRepository
                .GetAll()
                .FirstOrDefault(r => r.CustomerId == customerId && r.ProductId == productId);

            if (registration != null)
            {
                _registrationRepository.Delete(registration);
                _registrationRepository.Save();
                TempData["SuccessMessage"] = "Product unregistered successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "The product is not registered for this customer.";
            }

            return RedirectToAction("Registrations", new { customerId });
        }
    }
}
