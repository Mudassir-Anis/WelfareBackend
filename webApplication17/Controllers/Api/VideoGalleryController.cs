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
    public class VideoGalleryController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET()
        {
            try
            {
                var PhotoSliders = Models.ApiDBHelper.VideoGalleries().ToList().ConvertAll(row => {

                    return new VideoGallery()
                    {
                        Id = row.Id,
                        VideoPath = Url.Content("~/") + "Content/PhotoGallery/" + row.VideoPath,
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
