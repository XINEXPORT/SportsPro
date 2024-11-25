using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] 
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] 
        [HttpGet]
        [Route("about")]
        public ActionResult About()
        {
            return View();
        }

        [Authorize] 
        [HttpGet]
        [Route("dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize] 
        [HttpGet]
        [Route("settings")]
        public ActionResult Settings()
        {
            return View();
        }
    }
}
