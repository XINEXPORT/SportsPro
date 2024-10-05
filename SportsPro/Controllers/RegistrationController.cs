using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
