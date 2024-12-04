using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SportsPro.Models.ExtensionMethods;

namespace YourNamespace.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "asp-nav-filter")]
    public class ActiveNavPillTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; } = null!;

        [HtmlAttributeName("asp-nav-filter")]
        public string FilterValue { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Retrieve the current filter value from ViewData
            string currentFilter = ViewCtx.ViewData["Filter"]?.ToString() ?? string.Empty;

            // Compare the filter value to determine if it should be active
            if (string.Equals(currentFilter, FilterValue, StringComparison.OrdinalIgnoreCase))
            {
                output.Attributes.AppendCssClass("active");
            }
        }
    }
}
