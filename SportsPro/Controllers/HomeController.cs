using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("about")]
        public ActionResult About()
        {
            return View();
        }

    }
}