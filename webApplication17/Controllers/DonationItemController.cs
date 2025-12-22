using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class DonationItemController : Controller
    {
        // GET: DonationItem
        public ActionResult Index()
        {
            ViewBag.DonationCategories = new Models.DBHelper().GetDonationCategories();
            return View();   
        }

        public JsonResult Submit(DonationItem donation)
        {

            int response = -1;
            try
            {
                response = Models.DBHelper.SubmitDonationItem(donation);
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
                response = Models.DBHelper.DeleteDonationItem(Id);
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDonationItems()
        {
            var list = new Models.DBHelper().GetDonationItems();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}