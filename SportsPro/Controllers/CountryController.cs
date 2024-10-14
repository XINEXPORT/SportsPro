using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class CountryController : Controller
    {
        private SportsProContext context { get; set; }

        public CountryController(SportsProContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        // GET COUNTRIES
        public async Task<IActionResult> GetCountries()
        {
            var countries = await context.Countries.ToListAsync();
            ViewBag.Countries = countries;
            return View();
        }
    }
}
