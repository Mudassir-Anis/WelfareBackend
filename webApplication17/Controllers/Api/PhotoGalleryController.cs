using AlifSani.Models;
using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AlifSani.Controllers.Api
{
    public class PhotoGalleryController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET()
        {
            try
            {
                var PhotoSliders = Models.ApiDBHelper.PhotoGalleries().ToList().ConvertAll(row => {

                    return new PhotoGallery()
                    {
                        Id = row.Id,
                        Image = Url.Content("~/") + "Content/PhotoGallery/" + row.Image,
                        Status = row.Status,
                        CreatedDate = row.CreatedDate

                    };

                });
                return new WebApiResponse()
                {
                    IsSuccess = true,
                    Data = PhotoSliders,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new WebApiResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };

            }
            
        }
    }
}
