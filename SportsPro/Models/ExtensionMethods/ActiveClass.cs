using System;
using Microsoft.AspNetCore.Mvc.Rendering;

//Extensions methods allow to extend functionality without modifying source code.
//usally they are static methods used as if they were instance methods
//Active Class is used for the 

namespace SportsPro.Helpers
{
    public static class HtmlExtensions
    {
        public static string ActiveClass(
            this IHtmlHelper html,
            string controller,
            string action = ""
        )
        {
            var routeData = html.ViewContext.RouteData.Values;
            var currentController = routeData["controller"]?.ToString() ?? string.Empty;
            var currentAction = routeData["action"]?.ToString() ?? string.Empty;

            return
                string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase)
                && (
                    string.IsNullOrEmpty(action)
                    || string.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase)
                )
                ? "active"
                : string.Empty;
        }
    }
}
