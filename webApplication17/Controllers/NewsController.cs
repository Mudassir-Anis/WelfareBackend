using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();   
        }

        public JsonResult Submit(News News)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitNews(News);
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
                response = Models.DBHelper.DeleteNews(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNews()
        {
            var list = new Models.DBHelper().GetNews();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}