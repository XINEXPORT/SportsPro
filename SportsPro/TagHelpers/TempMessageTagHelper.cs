using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures; // [ViewContext] attribute
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsPro.Models.ExtensionMethods; // ViewContext class

namespace SportsPro.TagHelpers
{
    [HtmlTargetElement("my-temp-message")]
    public class TempMessageTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; } = null!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var td = ViewCtx.TempData;
            if (td.ContainsKey("message"))
            {
                output.BuildTag("h4", "bg-info text-center text-white p-2");
                output.Content.SetContent(td["message"]?.ToString());
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
