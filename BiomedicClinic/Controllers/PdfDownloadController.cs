using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiomedicClinic.Controllers
{
    public class PdfDownloadController : Controller
    {
        public ActionResult Download()
        {
            var filePath = Server.MapPath("~/DynamicContent/pdf/assesmform.pdf");
            return File(filePath, "application/pdf", "assesmform.pdf");
        }
    }
}