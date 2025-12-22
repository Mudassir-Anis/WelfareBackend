
using AlifSani.Models.EntityFramework;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlifSani.Controllers
{
    //[Authorize]
    //public class AlbumController : Controller
    //{
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public JsonResult Submit(
    //        PhotoAlbum album,
    //        HttpPostedFileBase Thumbnail,
    //        HttpPostedFileBase[] AlbumImages)
    //    {
    //        int response = -1;
    //        string message = "";

    //        try
    //        {
    //            response = Models.DBHelper.SubmitAlbum(album, Thumbnail, AlbumImages);
    //        }
    //        catch (Exception ex)
    //        {
    //            message = ex.Message;
    //        }

    //        return Json(new { Code = response, Message = message }, JsonRequestBehavior.AllowGet);
    //    }

    //    [HttpGet]
    //    public JsonResult GetAlbums()
    //    {
    //        var list = new Models.DBHelper().GetAlbums();
    //        return Json(new { data = list }, JsonRequestBehavior.AllowGet);
    //    }

    //    [HttpPost]
    //    public JsonResult Delete(int Id)
    //    {
    //        int response = Models.DBHelper.DeleteAlbum(Id);
    //        return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
    //    }
    //}

    [Authorize]
    public class AlbumController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Submit(
            PhotoAlbum album,
            HttpPostedFileBase Thumbnail,
            HttpPostedFileBase[] AlbumImages)
        {
            int response = -1;
            string message = "";

            try
            {
                response = Models.DBHelper.SubmitAlbum(album, Thumbnail, AlbumImages);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { Code = response, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAlbums()
        {
            var list = new Models.DBHelper().GetAlbums();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAlbumById(int id)
        {
            using (Entities db = new Entities())
            {
                var album = db.PhotoAlbums
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        x.Id,
                        x.AlbumName,
                        x.AlbumDescription,
                        x.Status
                    }).FirstOrDefault();

                return Json(album, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            int response = Models.DBHelper.DeleteAlbum(id);
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAlbumImages(int id)
        {
            using (Entities db = new Entities())
            {
                var images = db.PhotoAlbumImages
                    .Where(x => x.AlbumId == id)
                    .Select(x => new
                    {
                        x.Id,
                        x.Image
                    }).ToList();

                return Json(images, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteAlbumImage(int id)
        {
            using (Entities db = new Entities())
            {
                var img = db.PhotoAlbumImages.FirstOrDefault(x => x.Id == id);
                if (img == null) return Json(new { Code = -1 });

                string path = Server.MapPath("~/Content/Albums/" + img.Image);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                db.PhotoAlbumImages.Remove(img);
                db.SaveChanges();

                return Json(new { Code = 1 });
            }
        }


    }

}


