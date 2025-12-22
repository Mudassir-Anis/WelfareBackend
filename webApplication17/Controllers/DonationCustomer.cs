using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class DonationCustomerController : Controller
    {
        // GET: DonationCustomer
        public ActionResult Index()
        {
            return View();
        }
  
        [HttpGet]
        public ActionResult GetDonateCustomerInfos()
        {
            var list = new Models.DBHelper().GetDonateCustomerInfos().OrderByDescending(x=>x.CreatedDate).ToList();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}