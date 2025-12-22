using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class TaxDocController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Submit(TaxDoc taxDoc, HttpPostedFileBase File)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitTaxDoc(taxDoc, File);
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
                response = Models.DBHelper.DeleteTaxDoc(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTaxDocs()
        {
            var list = new Models.DBHelper().GetTaxDocs();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}