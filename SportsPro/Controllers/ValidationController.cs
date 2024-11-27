using Microsoft.AspNetCore.Mvc;
using SportsPro.Data.Configuration;
using SportsPro.Models;
using System.Linq;


namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        private Repository<Customer> data { get; set; }
        public ValidationController(SportsProContext ctx) => data = new Repository<Customer>(ctx);

        public JsonResult CheckEmail(string email, int customerID)
        {
            if (customerID == 0)  // only check for new customers - don't check on edit
            {
                string msg = Check.EmailExists(data, email);
                if (!string.IsNullOrEmpty(msg))
                {
                    return Json(msg);
                }
            }

            TempData["okEmail"] = true;
            return Json(true);
        }
    }
}

