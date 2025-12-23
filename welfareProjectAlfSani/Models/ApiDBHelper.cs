using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AlifSani.Models
{
    public class ApiDBHelper
    {
        public static IEnumerable<PhotoSlider> GetPhotoSliders()
        {
           
            using (Entities db = new Entities())
            {

                return db.PhotoSliders.Where(x => x.Status == true).ToList();


            }
        }
        public static IEnumerable<Service> Services()
        {

            using (Entities db = new Entities())
            {

                return db.Services.Where(x => x.Status == true).ToList();


            }
        }
        public static Service ServiceById(int id)
        {

            using (Entities db = new Entities())
            {

                return db.Services.Where(x => x.Id==id).FirstOrDefault();


            }
        }
        public static IEnumerable<FutureProject> FutureProjects()
        {

            using (Entities db = new Entities())
            {

                return db.FutureProjects.Where(x => x.Status == true).ToList();


            }
        }
        public static IEnumerable<AuditReport> AuditReports()
        {

            using (Entities db = new Entities())
            {

                return db.AuditReports.Where(x => x.Status == true).ToList();


            }
        }
        public static IEnumerable<TaxDoc> TaxDocs()
        {

            using (Entities db = new Entities())
            {

                return db.TaxDocs.Where(x => x.Status == true).ToList();


            }
        }
        public static IEnumerable<PhotoGallery> PhotoGalleries()
        {

            using (Entities db = new Entities())
            {

                return db.PhotoGalleries.Where(x => x.Status == true).ToList();


            }
        }

        public static IEnumerable<VideoGallery> VideoGalleries()
        {

            using (Entities db = new Entities())
            {

                return db.VideoGalleries.Where(x => x.Status == true).ToList();


            }
        }
        public static IEnumerable<Download> downloads()
        {

            using (Entities db = new Entities())
            {

                return db.Downloads.Where(x => x.Status == true).ToList();


            }
        }
        public static IEnumerable<News> News()
        {

            using (Entities db = new Entities())
            {

                return db.News.Where(x => x.Status == true).ToList();


            }
        }

        //public static IEnumerable<DonationCategory> DonationCategories()
        //{

        //    using (Entities db = new Entities())
        //    {

        //        return db.DonationCategories.Where(x => x.Status == true).ToList();


        //    }
        //}

        public static IEnumerable<DonationCategory> DonationCategories()
        {

            using (Entities db = new Entities())
            {

                return db.DonationCategories.Where(x => x.Status == true).ToList();


            }
        }

        public static IEnumerable<DonationCategoryModel> DonationCategoriesRealtion()
        {
            List<DonationCategoryModel> donationCategoryModels = null;

            

            var result = Models.ApiDBHelper.DonationCategories().ToList().ConvertAll(row => {

                return new DonationCategoryModel()
                {
                    Id = row.Id,
                    Text=row.Text,
                    Status = row.Status,
                    CreatedDate = row.CreatedDate,
                    DonationItems= new Entities().DonationItems.Where(x=>x.DonationCategoryId==row.Id).ToList()

                };

            });


            return result;


        }
        public static IEnumerable<DonationItem> DonationItems()
        {

            using (Entities db = new Entities())
            {

                return db.DonationItems.Where(x => x.Status == true).ToList();


            }
        }



        internal static FutureProject FutureProjectById(int id)
        {
            using (Entities db = new Entities())
            {
                return db.FutureProjects.Where(x => x.Id==id).FirstOrDefault();
            }
        }

        public static IEnumerable<BeneficiaryAccount> BeneficiaryAccounts()
        {

            using (Entities db = new Entities())
            {

                return db.BeneficiaryAccounts.Where(x => x.Status == true).ToList();


            }
        }
        public static IEnumerable<PressRelease> PressReleases()
        {

            using (Entities db = new Entities())
            {

                return db.PressReleases.Where(x => x.Status == true).ToList();


            }
        }

        public static int Comment(Comment comment)
        {
            int Response = 0;
            using (Entities db = new Entities())
            {
                comment.CreatedDate = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                Response = 1;
                return Response;
            }
        }

        public static int DonateCustomerInfo(DonateCustomerInfo donateCustomer)
        {
            int Response = 0;
            using (Entities db = new Entities())
            {
                donateCustomer.CreatedDate = DateTime.Now;
                db.DonateCustomerInfoes.Add(donateCustomer);
                db.SaveChanges();
                Response = 1;
                return Response;
            }
        }

        public static IEnumerable<PhotoAlbum> PhotoAlbums()
        {
            using (Entities db = new Entities())
            {
                return db.PhotoAlbums

                         .OrderByDescending(x => x.CreatedDate)
                         .ToList();
            }
        }

        public static IEnumerable<PhotoAlbumImage> PhotoAlbumImages(int albumId)
        {
            using (Entities db = new Entities())
            {
                return db.PhotoAlbumImages
                         .Where(x => x.AlbumId == albumId)
                         .ToList();
            }
        }


    }
}