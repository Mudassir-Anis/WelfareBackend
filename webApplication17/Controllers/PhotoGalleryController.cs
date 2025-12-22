using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class PhotoGalleryController : Controller
    {
        // GET: PhotoGallery
        public ActionResult Index()
        {
            return View();   
        }

        public JsonResult Submit(PhotoGallery photoGallery, HttpPostedFileBase FileName)
        {
            string message= "";
            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitPhotoGallery(photoGallery, FileName);
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
                response = Models.DBHelper.DeletePhotoGallery(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPhotoGalleries()
        {
            var list = new Models.DBHelper().GetPhotoGalleries();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}