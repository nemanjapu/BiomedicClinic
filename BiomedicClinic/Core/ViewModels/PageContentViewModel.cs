using BiomedicClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiomedicClinic.Core.ViewModels
{
    public class PageContentViewModel
    {
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string ImageToShow { get; set; }
        public string PageUrl { get; set; }

        public IEnumerable<WebsitePage> RelatedPages { get; set; }

        /*Page contents*/

        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }
        public string Content5 { get; set; }
    }
}