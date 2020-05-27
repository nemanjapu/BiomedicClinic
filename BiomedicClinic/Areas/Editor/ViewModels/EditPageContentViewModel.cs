using BiomedicClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiomedicClinic.Areas.Editor.ViewModels
{
    public class EditPageContentViewModel
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string ImageToShow { get; set; }

        public IEnumerable<WebsitePage> RelatedPages { get; set; }

        /*Page contents*/

        [AllowHtml]
        public string Content1 { get; set; }
        [AllowHtml]
        public string Content2 { get; set; }
        [AllowHtml]
        public string Content3 { get; set; }
        [AllowHtml]
        public string Content4 { get; set; }
        [AllowHtml]
        public string Content5 { get; set; }
    }
}