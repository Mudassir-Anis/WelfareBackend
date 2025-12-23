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
    public class DownloadController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET()
        {
            try
            {
                var services = Models.ApiDBHelper.downloads().ToList().ConvertAll(row => {

                    return new Download()
                    {
                        Id = row.Id,
                        Title=row.Title,
                        FileName = string.IsNullOrEmpty(row.FileName) ? "" : Url.Content("~/") + "Content/Attachment/" + row.FileName,
                        TitleImage = string.IsNullOrEmpty(row.TitleImage) ? "" : Url.Content("~/") + "Content/Attachment/" + row.TitleImage,
                        Status = row.Status,
                        CreatedDate = row.CreatedDate

                    };

                });
                return new WebApiResponse()
                {
                    IsSuccess = true,
                    Data = services,
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
