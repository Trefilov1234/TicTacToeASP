using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ClassWork.TagHelpers
{
    [HtmlTargetElement("a",Attributes ="cell")]
    public class CellTagHelper:TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        public CellTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }
        public string Cell { get; set; }
        public bool Blocked { get; set; }
        public bool ButtonBlocked { get; set; }
        public string Type { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext Context { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(Context);
            string url;
            output.TagName = "a";
            if(Type.Equals("start"))
            {
                if (ButtonBlocked)
                {
                    url = "";
                }
                else
                {
                    url = urlHelper.ActionLink("StartGame", "Home");
                }
                output.Attributes.Add("href", url);
            }
            if (Type.Equals("chooseMode"))
            {

                if (Cell.Equals("vsMan") || Cell.Equals("vsMachine"))
                {
                    url = urlHelper.ActionLink("ChooseMode", "Home", new { mode = Cell });
                    output.Attributes.Add("href", url);
                }
            }
            if (Type.Equals("cell"))
            {

                if (Blocked)
                {
                    output.Attributes.Add("class", "cell blocked");
                }
                else
                {
                    url = urlHelper.ActionLink("CellClick", "Home", new { cellId = Convert.ToInt32(Cell) });
                    output.Attributes.Add("href", url);
                    output.Attributes.Add("class", "cell");
                }
            }
            
        }
    }
}
