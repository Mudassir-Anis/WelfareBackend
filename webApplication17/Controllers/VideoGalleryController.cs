using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class VideoGalleryController : Controller
    {
        // GET: VideoGallery
        public ActionResult Index()
        {
            return View();   
        }

        public JsonResult Submit(VideoGallery VideoGallery, HttpPostedFileBase FileName)
        {
            string message= "";
            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitVideoGallery(VideoGallery, FileName);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                response = -1;
            }
            return Json(new { Code = response,Message= message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int Id)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.DeleteVideoGallery(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetVideoGalleries()
        {
            var list = new Models.DBHelper().GetVideoGalleries();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}