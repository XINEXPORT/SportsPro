using Microsoft.AspNetCore.Mvc;

namespace SportsPro.ViewComponents
{
    public class CopyrightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var year = DateTime.Now.Year;
            var companyName = "SportsPro";
            return View("Default", (year, companyName));
        }
    }
}
