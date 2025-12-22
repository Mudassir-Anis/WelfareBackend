using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class FutureProjectController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Submit(FutureProject futureProject, HttpPostedFileBase File)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitFutureProject(futureProject, File);
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
                response = Models.DBHelper.DeleteFutureProject(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFutureProjects()
        {
            var list = new Models.DBHelper().GetFutureProjects();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}