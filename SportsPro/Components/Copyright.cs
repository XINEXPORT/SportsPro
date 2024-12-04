// File: ViewComponents/CopyrightViewComponent.cs
using Microsoft.AspNetCore.Mvc;

namespace SportsPro.ViewComponents
{
    public class CopyrightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var year = DateTime.Now.Year;
            var companyName = "Your Company Name";
            return View("Default", (year, companyName));
        }
    }
}
