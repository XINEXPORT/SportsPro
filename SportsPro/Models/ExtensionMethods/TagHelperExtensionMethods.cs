using Microsoft.AspNetCore.Razor.TagHelpers;

//TagHelper extension methods help bind css classes with razor tag helper attributes
//Razor comes with a pre-built tag helpers; however, this is a custom tag helper builder

namespace SportsPro.Models.ExtensionMethods
{
    public static class TagHelperExtensions
    {

        //add a TagHelpAttribute to the existing list
        public static void AppendCssClass(this TagHelperAttributeList list, string newCssClasses)
        {
            string oldCssClasses = list["class"]?.Value.ToString() ?? "";
            string cssClasses = string.IsNullOrEmpty(oldCssClasses)
                ? newCssClasses
                : $"{oldCssClasses} {newCssClasses}";
            list.SetAttribute("class", cssClasses);
        }

        //build and configure the taghelper
        //add it to the atrribute list
        public static void BuildTag(this TagHelperOutput output, string tagName, string classNames)
        {
            output.TagName = tagName;
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.AppendCssClass(classNames);
        }

        //bind the taghelper with an a href html tag
        //EX: <a href="/submit" class="btn btn-primary">Submit</a>
        public static void BuildLink(this TagHelperOutput output, string url, string classNames)
        {
            output.BuildTag("a", classNames);
            output.Attributes.SetAttribute("href", url);
        }
    }
}
