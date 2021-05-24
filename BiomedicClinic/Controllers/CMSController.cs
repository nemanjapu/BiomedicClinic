using BiomedicClinic.Core;
using BiomedicClinic.Core.Models;
using BiomedicClinic.Core.ViewModels;
using BiomedicClinic.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BiomedicClinic.Controllers
{
    public class CMSController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CMSController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Sitemap

        public class SitemapNode
        {
            public SitemapFrequency? Frequency { get; set; }
            public DateTime? LastModified { get; set; }
            public double? Priority { get; set; }
            public string Url { get; set; }
        }

        public enum SitemapFrequency
        {
            Never,
            Yearly,
            Monthly,
            Weekly,
            Daily,
            Hourly,
            Always
        }

        public IReadOnlyCollection<SitemapNode> GetSitemapNodes(UrlHelper urlHelper)
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            var items = _unitOfWork.WebsitePages.GetPagesForSitemap().Select(l => new
            {
                url = l.PageUrl
            }).ToList();
            foreach (var item in items)
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = "https://www.biomedic.co.uk" + item.url,
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 1
                   });
            }

            return nodes;
        }

        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }

        public ActionResult SitemapXml()
        {
            var sitemapNodes = GetSitemapNodes(this.Url);
            string xml = GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "xml", Encoding.UTF8);
        }

        #endregion

        //[HtmlActionFilter]
        public ActionResult ReturnCMSPage(string url)
        {
            //IF WE'RE CALLING FOR HOMEPAGE, URL WOULD BE NULL, SO WE'RE SETTING IT TO JUST AN EMPTY STRING
            //if (String.IsNullOrEmpty(url))
            //{
            //    url = "";
            //}

            //ENABLING URL'S WITH AND WITHOUT TRAILING SLASH, 
            //SECOND CONDITION IS IF WE'RE CALLING FOR HOMEPAGE THEN 
            //DON'T ADD TRAILING SLASH BECAUSE WE'RE ADDING IT IN THE REPOSITORY
            //if (!url.EndsWith("/") && !String.IsNullOrEmpty(url))
            //{
            //    url = url + "/";
            //}
            //var websitePageDB = _unitOfWork.WebsitePages.GetPageByUrl(url);

            var websitePageDB = HttpContext.Items["cmspage"] as WebsitePage;

            //IF WE CAN'T FIND PAGE IN DATABASE WITH THE PROVIDED URL, REDIRECT TO HOMEPAGE
            //if (websitePageDB == null)
            //{
            //    return Redirect("/");
            //}

            //if (!string.IsNullOrEmpty(websitePageDB.Content1))
            //{
            //    //replace non-ascii with empty string
            //    websitePageDB.Content1 = Regex.Replace(websitePageDB.Content1, @"[^\u0000-\u007F]", string.Empty);

            //    //replace 3 or more BR with one BR
            //    websitePageDB.Content1 = Regex.Replace(websitePageDB.Content1, "(?:\\s*<br[/\\s]*>\\s*){3,}", "");

            //    //remove any style attributes   
            //    websitePageDB.Content1 = Regex.Replace(websitePageDB.Content1, " style=(\"|')[^(\"|')]*(\"|')", "");

            //    //remove empty p tags
            //    websitePageDB.Content1 = Regex.Replace(websitePageDB.Content1, "(<p>\\s*</p>|<p>\\s*​\\?</p>|<p>&nbsp;</p>)", "<br>");

            //    //remove font tags
            //    websitePageDB.Content1 = Regex.Replace(websitePageDB.Content1, "</?(font)[^>]*>", "");
            //}

            var websitePageToRedirectTo = new PageContentViewModel()
            {
                Content1 = websitePageDB.Content1,
                Content2 = websitePageDB.Content2,
                Content3 = websitePageDB.Content3,
                Content4 = websitePageDB.Content4,
                Content5 = websitePageDB.Content5,
                ImageToShow = string.IsNullOrEmpty(websitePageDB.ImagePath) ? "Content/images/bioreg-page-bg.jpg" : websitePageDB.ImagePath + "/" + websitePageDB.ImageName,
                MetaDescription = websitePageDB.MetaDescription,
                MetaKeywords = websitePageDB.MetaKeywords,
                PageUrl = websitePageDB.PageUrl,
                PageTitle = websitePageDB.PageTitle,
                RelatedPages = _unitOfWork.WebsitePages.GetRelatedPages(websitePageDB.Id, websitePageDB.ParentId).ToList()
            };

            string template = websitePageDB.Template;
            return View(template, websitePageToRedirectTo);
        }

        public ActionResult GetSideMenuItems()
        {
            var model = _unitOfWork.WebsitePages.GetActivePagesByMenuId(2).OrderBy(m => m.SortOrder).ThenBy(m => m.Id);

            return PartialView("_SideMenuPartial", model);
        }

        public ActionResult GetSocialMedia()
        {
            var model = _unitOfWork.GlobalSettings.GetGlobalValues();

            return PartialView("_SocialMediaPartial", model);
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
                    if (page2.ParentId == pageId)
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