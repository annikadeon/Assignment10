using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Infrastructure
{
    //specify what kind of elemnet will apply to the tag helper
    [HtmlTargetElement("div", Attributes= "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;
        //constructor, passing in info about the URL
        public PaginationTagHelper (IUrlHelperFactory url)
        {
            urlInfo = url;
        }
        //use atrribute pageinfo to set information, for index.cshtml
        //pass in the page numer info data, which will be store in Pageinfo property in PaginationTagHelper
        public PageNumberInfo PageInfo { get; set; }
        //dictionary key values pairs
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        //these are used for the button to make it look better
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }



        //process method, refers to what we are going to do
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //build urls automatically
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);


            //info passed from home controller, through the view, to the tag helper in order to send the view the info about the page
            //build div and a tag
            TagBuilder finishedTag = new TagBuilder("div");
            //set up information for a tag


            for (int i=1; i <= PageInfo.NumPages; i++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                
                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    individualTag.AddCssClass(i == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }
               
                individualTag.InnerHtml.Append(i.ToString());
                //add the a tag into the div tag
                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            //append all to html
            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
