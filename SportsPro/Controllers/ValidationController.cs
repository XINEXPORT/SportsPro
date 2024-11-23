using Microsoft.AspNetCore.Mvc;
using SportsPro.Data;
using SportsPro.Models;
using System.Linq;

namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        public IActionResult CheckProductCode(string code, [FromServices] IRepository<Product> productRepo)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Json("Code cannot be empty.");
            }

            var product = productRepo.GetAll().FirstOrDefault(p => p.ProductCode == code);
            if (product != null)
            {
                return Json($"The code '{code}' already exists.");
            }

            return Json(true);
        }
    }
}
