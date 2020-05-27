using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiomedicClinic.Core.Models
{
    public class WebsitePage
    {
        public int Id { get; set; }
        public string PageUrl { get; set; }
        public string PageExternalUrl { get; set; }
        public bool OpenPageInNewTab { get; set; }
        public string MenuName { get; set; }
        public string PageTitle { get; set; }
        public string Template { get; set; }
        public int SortOrder { get; set; }
        public int ParentId { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public bool isHidden { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        /*Page contents*/

        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }
        public string Content5 { get; set; }
    }
}