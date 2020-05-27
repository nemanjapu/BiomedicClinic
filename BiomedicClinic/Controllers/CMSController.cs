using BiomedicClinic.Core;
using BiomedicClinic.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiomedicClinic.Controllers
{
    public class CMSController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CMSController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult ReturnCMSPage(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                string viewName = "Homepage";
                return View(viewName);
            }

            if (String.IsNullOrEmpty(url))
            {
                string viewName = "Homepage";
                return View(viewName);
            }
            else
            {
                var websitePageDB = _unitOfWork.WebsitePages.GetPageByUrl(url);
                if(websitePageDB == null)
                {
                    return Redirect("/");
                }
                var websitePageToRedirectTo = new PageContentViewModel()
                {
                    Content1 = websitePageDB.Content1,
                    Content2 = websitePageDB.Content2,
                    Content3 = websitePageDB.Content3,
                    Content4 = websitePageDB.Content4,
                    Content5 = websitePageDB.Content5,
                    ImageToShow = string.IsNullOrEmpty(websitePageDB.ImagePath) ? "Content/images/bioreg-page-bg.jpg" : websitePageDB.ImagePath + "/" + websitePageDB.ImageName,
                    MetaDescription = websitePageDB.MetaDescription,
                    PageTitle = websitePageDB.PageTitle,
                    RelatedPages = _unitOfWork.WebsitePages.GetRelatedPages(websitePageDB.Id, websitePageDB.ParentId).ToList()
                };

                string template = websitePageDB.Template;
                return View(template, websitePageToRedirectTo);
            }
        }

        public ActionResult GetSideMenuItems()
        {
            var model = _unitOfWork.WebsitePages.GetActivePagesByMenuId(2).OrderBy(m => m.SortOrder).ThenBy(m => m.Id);

            return PartialView("_SideMenuPartial", model);
        }

        public ActionResult GetTopMenuItems()
        {
            var dbPage = _unitOfWork.WebsitePages.GetActivePagesByMenuId(1).OrderBy(m => m.SortOrder).ThenBy(m => m.Id);

            var model = dbPage.Select(m => new MenuItemsViewModel
            {
                ParentId = m.ParentId,
                Id = m.Id,
                isHidden = m.isHidden,
                MenuName = m.MenuName,
                OpenPageInNewTab = m.OpenPageInNewTab,
                PageUrl = m.PageUrl,
                PageExternalUrl = m.PageExternalUrl,
                hasChildren = false
            }).ToList();

            foreach (var page in model)
            {
                int pageId = page.Id;
                foreach (var page2 in model)
                {
                    if(page2.ParentId == pageId)
                    {
                        page.hasChildren = true;
                        break;
                    }
                }
            }

            return PartialView("_TopMenuPartial", model);
        }
    }
}