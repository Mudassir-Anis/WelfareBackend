using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class AuditReportController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Submit(AuditReport auditReport, HttpPostedFileBase File)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitAuditReport(auditReport, File);
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
                response = Models.DBHelper.DeleteAuditReport(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAuditReports()
        {
            var list = new Models.DBHelper().GetAuditReports();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}