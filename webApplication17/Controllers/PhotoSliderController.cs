using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class PhotoSliderController : Controller
    {
        // GET: PhotoSlider
        public ActionResult Index()
        {
            return View();   
        }

        public JsonResult Submit(PhotoSlider photoSlider, HttpPostedFileBase FileName)
        {
            string message = "";
            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitPhotoSlider(photoSlider,FileName);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                response = -1;
            }
            return Json(new { Code = response,Message=message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int Id)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.DeletePhotoSlider(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPhotoSliders()
        {
            var list = new Models.DBHelper().GetPhotoSliders();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}