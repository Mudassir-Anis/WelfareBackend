using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class BeneficiaryAccountController : Controller
    {
        // GET: BeneficiaryAccount
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Submit(BeneficiaryAccount beneficiaryAccount)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitBeneficiaryAccount(beneficiaryAccount);
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
                response = Models.DBHelper.DeleteBeneficiaryAccount(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBeneficiaryAccounts()
        {
            var list = new Models.DBHelper().GetBeneficiaryAccounts();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}