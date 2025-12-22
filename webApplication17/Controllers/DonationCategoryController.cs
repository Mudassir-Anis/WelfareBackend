using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class DonationCategoryController : Controller
    {
        // GET: DonationCategory
        public ActionResult Index()
        {
            return View();   
        }

        public JsonResult Submit(DonationCategory donation)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitDonationCategory(donation);
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
                response = Models.DBHelper.DeleteDonationCategory(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDonationCategories()
        {
            var list = new Models.DBHelper().GetDonationCategories();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}