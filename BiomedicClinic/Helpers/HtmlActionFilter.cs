using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BiomedicClinic.Helpers
{
    public class HtmlActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting
        (ActionExecutingContext filterContext)
        {
            var originalFilter =
                filterContext.HttpContext.Response.Filter;
            filterContext.HttpContext.Response.Filter =
            new KeywordStream(originalFilter);
        }
    }
}