using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class PressReleaseController : Controller
    {
        // GET: PressRelease
        public ActionResult Index()
        {
            return View();   
        }

        public JsonResult Submit(PressRelease pressRelease, HttpPostedFileBase FileName)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitPressRelease(pressRelease, FileName);
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
                response = Models.DBHelper.DeletePressRelease(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPressReleases()
        {
            var list = new Models.DBHelper().GetPressReleases();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}