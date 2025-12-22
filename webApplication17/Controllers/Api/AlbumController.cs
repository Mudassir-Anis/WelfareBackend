using AlifSani.Models;
using AlifSani.Models.EntityFramework;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AlifSani.Controllers.Api
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Album")]
    public class AlbumController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        [Route("GetAlbums")]
        public WebApiResponse GetAlbums()
        {
            try
            {
                var albums = ApiDBHelper.PhotoAlbums()
                    .ToList()
                    .ConvertAll(row => new
                    {
                        Id = row.Id,
                        AlbumName = row.AlbumName,
                        AlbumDescription = row.AlbumDescription,
                        Thumbnail = Url.Content("~/") + "Content/Albums/" + row.Thumbnail,
                        CreatedDate = row.CreatedDate
                    });

                return new WebApiResponse
                {
                    IsSuccess = true,
                    Data = albums,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new WebApiResponse
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        [BasicAuthentication]
        [HttpGet]
        [Route("GetAlbumImages/{albumId}")]
        public WebApiResponse GetAlbumImages(int albumId)
        {
            try
            {
                var images = ApiDBHelper.PhotoAlbumImages(albumId)
                    .ToList()
                    .ConvertAll(row => new
                    {
                        row.Id,
                        Image = Url.Content("~/") + "Content/Albums/" + row.Image
                    });

                return new WebApiResponse
                {
                    IsSuccess = true,
                    Data = images,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new WebApiResponse
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }
    }
}
