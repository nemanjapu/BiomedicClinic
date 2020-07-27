using BiomedicClinic.Areas.Editor.ViewModels;
using BiomedicClinic.Core;
using BiomedicClinic.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiomedicClinic.Areas.Editor.Controllers
{
    [Authorize]
    public class CMSEditorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CMSEditorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Editor/CMSEditor
        public ActionResult ReturnCMSPageEdit(string url)
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
                if (websitePageDB == null)
                {
                    return Redirect("/");
                }
                var websitePageToRedirectTo = new EditPageContentViewModel()
                {
                    Id = websitePageDB.Id,
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
            var model = _unitOfWork.WebsitePages.GetActivePagesByMenuId(2);

            return PartialView("_SideMenuPartial", model);
        }

        public ActionResult GetTopMenuItems()
        {
            var dbPage = _unitOfWork.WebsitePages.GetActivePagesByMenuId(1);

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
                    if (page2.ParentId == pageId)
                    {
                        page.hasChildren = true;
                        break;
                    }
                }
            }

            return PartialView("_TopMenuPartial", model);
        }
        public ActionResult SaveCMSPageContent(EditPageContentViewModel model)
        {
            var websitePageDb = _unitOfWork.WebsitePages.GetPageById(model.Id);

            websitePageDb.Content1 = model.Content1;
            websitePageDb.Content2 = model.Content2;
            websitePageDb.Content3 = model.Content3;

            _unitOfWork.Complete();

            return RedirectToAction("ReturnCMSPageEdit", new { @url = websitePageDb.PageUrl });
        }
    }
}