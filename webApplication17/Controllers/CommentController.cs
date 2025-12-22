using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
  
        [HttpGet]
        public ActionResult GetComments()
        {
            var list = new Models.DBHelper().GetComments();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}