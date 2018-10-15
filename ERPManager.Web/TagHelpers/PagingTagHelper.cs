using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPManager.Web.TagHelpers
{
    [HtmlTargetElement("item-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }

        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("page-index")]
        public int PageIndex { get; set; }

        [HtmlAttributeName("page-controller")]
        public string Controller  { get; set; }

        [HtmlAttributeName("page-action")]
        public string Action { get; set; }

        [HtmlAttributeName("page-companyId")]
        public string CompanyId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class=\"pagination pagination-no-border\">");

            for(int i = 0; i< PageCount; i++ )
            {
                stringBuilder.AppendFormat("<li class='{0} page-item'>", i == PageIndex ? "active" : "");
                stringBuilder.AppendFormat("<a href='/{0}/{1}?page={2}&companyId={3}'>{4}</a>",
                    Controller, Action, CompanyId, i, i+1);
                stringBuilder.Append("</li>");
            }
            stringBuilder.Append("</ul>");

            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }
    }
}
