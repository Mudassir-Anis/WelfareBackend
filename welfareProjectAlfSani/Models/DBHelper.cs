using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;


namespace AlifSani.Models
{
    public class DBHelper
    {
        public IEnumerable<PhotoSlider> GetPhotoSliders()
        {
            List<PhotoSlider> releases = new List<PhotoSlider>();
            using (Entities db = new Entities())
            {

                return db.PhotoSliders.ToList();
            }
        }
        public IEnumerable<Service> GetServices()
        {
            List<Service> releases = new List<Service>();
            using (Entities db = new Entities())
            {

                return db.Services.ToList();
            }
        }
        public IEnumerable<FutureProject> GetFutureProjects()
        {
            
            using (Entities db = new Entities())
            {

                return db.FutureProjects.ToList();
            }
        }
        public IEnumerable<AuditReport> GetAuditReports()
        {

            using (Entities db = new Entities())
            {

                return db.AuditReports.ToList();
            }
        }

        public IEnumerable<TaxDoc> GetTaxDocs()
        {

            using (Entities db = new Entities())
            {

                return db.TaxDocs.ToList();
            }
        }
        public IEnumerable<PhotoGallery> GetPhotoGalleries()
        {

            using (Entities db = new Entities())
            {

                return db.PhotoGalleries.ToList();
            }
        }
        public IEnumerable<PressRelease> GetPressReleases()
        {

            using (Entities db = new Entities())
            {

                return db.PressReleases.ToList();
            }
        }

        public IEnumerable<BeneficiaryAccount> GetBeneficiaryAccounts()
        {

            using (Entities db = new Entities())
            {

                return db.BeneficiaryAccounts.ToList();
            }
        }
        public IEnumerable<VideoGallery> GetVideoGalleries()
        {

            using (Entities db = new Entities())
            {

                return db.VideoGalleries.ToList();
            }
        }
        public IEnumerable<News> GetNews()
        {

            using (Entities db = new Entities())
            {

                return db.News.ToList();
            }
        }

        public IEnumerable<DonationCategory> GetDonationCategories()
        {

            using (Entities db = new Entities())
            {

                return db.DonationCategories.ToList();
            }
        }

        public IEnumerable<DonationItem> GetDonationItems()
        {

            using (Entities db = new Entities())
            {

                return db.DonationItems.ToList();
            }
        }

        public IEnumerable<DonateCustomerInfo> GetDonateCustomerInfos()
        {

            using (Entities db = new Entities())
            {

                return db.DonateCustomerInfoes.ToList();
            }
        }
        public IEnumerable<Download> GetDownloads()
        {

            using (Entities db = new Entities())
            {

                return db.Downloads.ToList();
            }
        }

        public IEnumerable<Comment> GetComments()
        {

            using (Entities db = new Entities())
            {

                return db.Comments.ToList();
            }
        }

        internal static int SubmitPhotoSlider(PhotoSlider photoSlider, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/PhotoSlider/"), "PhotoSlider" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "PhotoSlider" + "_" + id.ToString() + fileExtension;

                }

                if (photoSlider.Id == 0)
                {
                    photoSlider.Image = path;
                    photoSlider.CreatedDate = DateTime.Now;
                    db.PhotoSliders.Add(photoSlider);
                    db.SaveChanges();

                }
                else 
                {
                    var obj = db.PhotoSliders.Where(x => x.Id == photoSlider.Id).FirstOrDefault();
                    obj.Status = photoSlider.Status;
                    if (path!="")
                    {
                        obj.Image = path;
                    }
                    db.SaveChanges();

                }

               
                Response = 1;

            }


            return Response;
        }

        internal static int SubmitDownload(Download download, HttpPostedFileBase fileName, HttpPostedFileBase titleImage)
        {
            string path = "";
            string path2 = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                

                if (fileName != null)
                {
                    string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");
                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Attachment/"), "Download" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "Download" + "_" + id.ToString() + fileExtension;

                }
                if (titleImage != null)
                {
                    string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");
                    string fileExtension = System.IO.Path.GetExtension(titleImage.FileName);
                    path2 = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Attachment/"), "Download" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path2);
                    // file is uploaded
                    path2 = "Download" + "_" + id.ToString() + fileExtension;

                }

                if (download.Id == 0)
                {
                    download.FileName = path;
                    download.TitleImage = path2;
                    download.CreatedDate = DateTime.Now;
                    db.Downloads.Add(download);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.Downloads.Where(x => x.Id == download.Id).FirstOrDefault();
                    obj.Status = download.Status;
                    obj.Title = download.Title;
                    if (path != "")
                    {
                        obj.FileName = path;
                    }
                    if (path2 != "")
                    {
                        obj.TitleImage = path2;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitPhotoGallery(PhotoGallery photoGallery, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/PhotoGallery/"), "PhotoGallery" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "PhotoGallery" + "_" + id.ToString() + fileExtension;

                }

                if (photoGallery.Id == 0)
                {
                    photoGallery.Image = path;
                    photoGallery.CreatedDate = DateTime.Now;
                    db.PhotoGalleries.Add(photoGallery);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.PhotoGalleries.Where(x => x.Id == photoGallery.Id).FirstOrDefault();
                    obj.Status = photoGallery.Status;
                    if (path != "")
                    {
                        obj.Image = path;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitVideoGallery(VideoGallery videoGallery, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/PhotoGallery/"), "VideoGallery" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "VideoGallery" + "_" + id.ToString() + fileExtension;

                }

                if (videoGallery.Id == 0)
                {
                    videoGallery.VideoPath = path;
                    videoGallery.CreatedDate = DateTime.Now;
                    db.VideoGalleries.Add(videoGallery);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.PhotoGalleries.Where(x => x.Id == videoGallery.Id).FirstOrDefault();
                    obj.Status = videoGallery.Status;
                    if (path != "")
                    {
                        obj.Image = path;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitService(Service service, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Services/"), "Service" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "Service" + "_" + id.ToString() + fileExtension;

                }

                if (service.Id == 0)
                {
                    service.Image = path;
                    service.CreatedDate = DateTime.Now;
                    db.Services.Add(service);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.Services.Where(x => x.Id == service.Id).FirstOrDefault();
                    obj.Status = service.Status;
                    obj.Name = service.Name;
                    obj.Type = service.Type;
                    obj.Text = service.Text;
                    if (path != "")
                    {
                        obj.Image = path;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitFutureProject(FutureProject futureProject, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/FutureProject/"), "FutureProject" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "FutureProject" + "_" + id.ToString() + fileExtension;

                }

                if (futureProject.Id == 0)
                {
                    futureProject.Image = path;
                    futureProject.CreatedDate = DateTime.Now;
                    db.FutureProjects.Add(futureProject);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.FutureProjects.Where(x => x.Id == futureProject.Id).FirstOrDefault();
                    obj.Status = futureProject.Status;
                    obj.Name = futureProject.Name;
                    //obj.Type = futureProject.Type;
                    obj.Text = futureProject.Text;
                    if (path != "")
                    {
                        obj.Image = path;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitAuditReport(AuditReport auditReport, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Attachment/"), "AuditReport" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "AuditReport" + "_" + id.ToString() + fileExtension;

                }

                if (auditReport.Id == 0)
                {
                    auditReport.FileName = path;
                    auditReport.CreatedDate = DateTime.Now;
                    db.AuditReports.Add(auditReport);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.AuditReports.Where(x => x.Id == auditReport.Id).FirstOrDefault();
                    obj.Status = auditReport.Status;
                    obj.Title = auditReport.Title;
                    if (path != "")
                    {
                        obj.FileName = path;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }
        internal static int SubmitTaxDoc(TaxDoc taxDoc, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Attachment/"), "TaxDoc" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "TaxDoc" + "_" + id.ToString() + fileExtension;

                }

                if (taxDoc.Id == 0)
                {
                    taxDoc.FileName = path;
                    taxDoc.CreatedDate = DateTime.Now;
                    db.TaxDocs.Add(taxDoc);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.TaxDocs.Where(x => x.Id == taxDoc.Id).FirstOrDefault();
                    obj.Status = taxDoc.Status;
                    obj.Title = taxDoc.Title;
                  
                    if (path != "")
                    {
                        obj.FileName = path;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitPressRelease(PressRelease pressRelease, HttpPostedFileBase fileName)
        {
            string path = "";
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (fileName != null)
                {

                    string fileExtension = System.IO.Path.GetExtension(fileName.FileName);
                    path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/PressRelease/"), "PressRelease" + "_" + id.ToString() + fileExtension);
                    fileName.SaveAs(path);
                    // file is uploaded
                    path = "PressRelease" + "_" + id.ToString() + fileExtension;

                }

                if (pressRelease.Id == 0)
                {
                    pressRelease.FileName = path;
                    pressRelease.CreatedDate = DateTime.Now;
                    db.PressReleases.Add(pressRelease);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.PressReleases.Where(x => x.Id == pressRelease.Id).FirstOrDefault();
                    obj.Status = pressRelease.Status;
                    obj.Title = pressRelease.Title;

                    if (path != "")
                    {
                        obj.FileName = path;
                    }
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitBeneficiaryAccount(BeneficiaryAccount beneficiary)
        {
           
            int Response = -1;


            using (Entities db = new Entities())
            {
                string id = DateTime.Now.ToString("dd-MM-yyyy-mm-ss");

                if (beneficiary.Id == 0)
                {
                    
                    beneficiary.CreatedDate = DateTime.Now;
                    db.BeneficiaryAccounts.Add(beneficiary);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.BeneficiaryAccounts.Where(x => x.Id == beneficiary.Id).FirstOrDefault();
                    obj.AccountNo = beneficiary.AccountNo;
                    obj.AccountTitle = beneficiary.AccountTitle;
                    obj.Address = obj.Address;
                    obj.Bank = beneficiary.Bank;
                    obj.Branch = beneficiary.Branch;
                    obj.BranchCode = beneficiary.BranchCode;
                    obj.Email = beneficiary.Email;
                    obj.Fax = beneficiary.Fax;
                    obj.Phone = beneficiary.Phone;
                    obj.UAN = beneficiary.UAN;
                    obj.Status = beneficiary.Status;
                    db.SaveChanges();
                    
                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitNews(News news)
        {

            int Response = -1;


            using (Entities db = new Entities())
            {
                

                if (news.Id == 0)
                {

                    news.CreatedDate = DateTime.Now;
                    db.News.Add(news);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.News.Where(x => x.Id == news.Id).FirstOrDefault();
                    obj.Title = news.Title;
                    obj.Text = news.Text;
                    obj.Date = news.Date;
                    obj.Status = news.Status;
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }

        internal static int SubmitDonationCategory(DonationCategory donationCategory)
        {

            int Response = -1;


            using (Entities db = new Entities())
            {


                if (donationCategory.Id == 0)
                {

                    donationCategory.CreatedDate = DateTime.Now;
                    db.DonationCategories.Add(donationCategory);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.DonationCategories.Where(x => x.Id == donationCategory.Id).FirstOrDefault();
                    obj.Text = donationCategory.Text;
                    obj.Status = donationCategory.Status;
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }
        internal static int SubmitDonationItem(DonationItem item)
        {

            int Response = -1;


            using (Entities db = new Entities())
            {


                if (item.Id == 0)
                {

                    item.CreatedDate = DateTime.Now;
                    db.DonationItems.Add(item);
                    db.SaveChanges();

                }
                else
                {
                    var obj = db.DonationItems.Where(x => x.Id == item.Id).FirstOrDefault();
                    obj.Price = item.Price;
                    obj.Title = item.Title;
                    obj.Status = item.Status;
                    db.SaveChanges();

                }


                Response = 1;

            }


            return Response;
        }
       

        internal static int DeletePhotoSlider(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.PhotoSliders.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }

        internal static int DeleteVideoGallery(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.VideoGalleries.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }
        internal static int DeleteNews(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.News.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }
        internal static int DeleteDownloads(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.Downloads.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }

        internal static int DeleteService(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.Services.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }


        internal static int DeleteDonationCategory(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.DonationCategories.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }
        internal static int DeleteDonationItem(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.DonationItems.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }

        internal static int DeleteFutureProject(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.FutureProjects.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }
        internal static int DeleteAuditReport(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.AuditReports.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }
        internal static int DeleteTaxDoc(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.Services.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }

        internal static int DeletePhotoGallery(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.PhotoGalleries.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }

        internal static int DeleteBeneficiaryAccount(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.BeneficiaryAccounts.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }

        internal static int DeletePressRelease(int id)
        {
            int Response = -1;
            using (Entities db = new Entities())
            {
                var obj = db.PressReleases.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
                Response = 1;



            }
            return Response;
        }

        //internal static int SubmitAlbum(
        //        PhotoAlbum album,
        //        HttpPostedFileBase thumbnail,
        //        HttpPostedFileBase[] albumImages)
        //{
        //    using (Entities db = new Entities())
        //    {
        //        string albumFolder = album.AlbumName.Replace(" ", "_");
        //        string basePath = HttpContext.Current.Server.MapPath("~/Content/Albums/" + albumFolder);

        //        if (!Directory.Exists(basePath))
        //            Directory.CreateDirectory(basePath);

        //        // Save Thumbnail
        //        if (thumbnail != null)
        //        {
        //            string thumbName = "thumb_" + Guid.NewGuid() + Path.GetExtension(thumbnail.FileName);
        //            string thumbPath = Path.Combine(basePath, thumbName);
        //            thumbnail.SaveAs(thumbPath);
        //            album.Thumbnail = albumFolder + "/" + thumbName;
        //        }

        //        album.CreatedDate = DateTime.Now;
        //        db.PhotoAlbums.Add(album);
        //        db.SaveChanges();

        //        // Save Album Images
        //        if (albumImages != null)
        //        {
        //            foreach (var img in albumImages)
        //            {
        //                if (img == null) continue;

        //                string imgName = Guid.NewGuid() + Path.GetExtension(img.FileName);
        //                string imgPath = Path.Combine(basePath, imgName);
        //                img.SaveAs(imgPath);

        //                db.PhotoAlbumImages.Add(new PhotoAlbumImage
        //                {
        //                    AlbumId = album.Id,
        //                    Image = albumFolder + "/" + imgName,
        //                    CreatedDate = DateTime.Now
        //                });
        //            }
        //            db.SaveChanges();
        //        }

        //        return 1;
        //    }
        //}


        internal static int SubmitAlbum(
    PhotoAlbum album,
    HttpPostedFileBase thumbnail,
    HttpPostedFileBase[] albumImages)
        {
            using (Entities db = new Entities())
            {
                PhotoAlbum obj;

                if (album.Id == 0)
                {
                    obj = album;
                    obj.CreatedDate = DateTime.Now;
                    db.PhotoAlbums.Add(obj);
                    db.SaveChanges();
                }
                else
                {
                    obj = db.PhotoAlbums.FirstOrDefault(x => x.Id == album.Id);
                    obj.AlbumName = album.AlbumName;
                    obj.AlbumDescription = album.AlbumDescription;
                    obj.Status = album.Status;
                    db.SaveChanges();
                }

                string folder = obj.AlbumName.Replace(" ", "_");
                string basePath = HttpContext.Current.Server.MapPath("~/Content/Albums/" + folder);

                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                // Thumbnail (optional on edit)
                if (thumbnail != null)
                {
                    string thumbName = "thumb_" + Guid.NewGuid() + Path.GetExtension(thumbnail.FileName);
                    thumbnail.SaveAs(Path.Combine(basePath, thumbName));
                    obj.Thumbnail = folder + "/" + thumbName;
                    db.SaveChanges();
                }

                // Add more images
                if (albumImages != null)
                {
                    foreach (var img in albumImages)
                    {
                        if (img == null) continue;

                        string imgName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                        img.SaveAs(Path.Combine(basePath, imgName));

                        db.PhotoAlbumImages.Add(new PhotoAlbumImage
                        {
                            AlbumId = obj.Id,
                            Image = folder + "/" + imgName,
                            CreatedDate = DateTime.Now
                        });
                    }
                    db.SaveChanges();
                }

                return 1;
            }
        }


        internal object GetAlbums()
        {
            using (Entities db = new Entities())
            {
                return db.PhotoAlbums
                    .Select(x => new {
                        x.Id,
                        x.AlbumName,
                        x.AlbumDescription,
                        x.Thumbnail,
                        x.Status
                    }).ToList();
            }
        }

        internal static int DeleteAlbum(int id)
        {
            using (Entities db = new Entities())
            {
                var album = db.PhotoAlbums.FirstOrDefault(x => x.Id == id);
                if (album == null) return -1;

                db.PhotoAlbums.Remove(album);
                db.SaveChanges();
                return 1;
            }
        }


    }
}