using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiomedicClinic.Helpers
{
    public class DropdownHelper
    {
        public static List<SelectListItem> GetPageTemplateList()
        {
            var listPageTemplate = new List<SelectListItem>
            {
                new SelectListItem{Text = "Blank Page", Value = "OnlyTextTemplate", Selected = true},
                new SelectListItem{Text = "Homepage", Value = "Homepage"},
                new SelectListItem{Text = "Parent Page", Value = "ParentTemplate"},
                new SelectListItem{Text = "Media", Value = "MediaTemplate"},
                new SelectListItem{Text = "Press", Value = "PressTemplate"}
            };

            return listPageTemplate;
        }
    }
}