using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index()
        {
            return View();   
        }

        public JsonResult Submit(Download Download, HttpPostedFileBase FileName, HttpPostedFileBase TitleImage)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitDownload(Download, FileName, TitleImage);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int Id)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.DeleteDownloads(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDownloads()
        {
            var list = new Models.DBHelper().GetDownloads();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}