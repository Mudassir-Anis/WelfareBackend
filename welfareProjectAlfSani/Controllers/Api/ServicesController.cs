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
    public class ServicesController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET()
        {
            try
            {
                var services = Models.ApiDBHelper.Services().ToList().ConvertAll(row => {

                    return new Service()
                    {
                        Id = row.Id,
                        Name=row.Name,
                        Text=row.Text,
                        Type=row.Type,
                        Image = string.IsNullOrEmpty(row.Image) ? "" : Url.Content("~/") + "Content/Services/" + row.Image,
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

        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET(int id)
        {
            try
            {


                var service = Models.ApiDBHelper.ServiceById(id);
                service.Image = string.IsNullOrEmpty(service.Image) ? "" : Url.Content("~/") + "Content/Services/" + service.Image;
                return new WebApiResponse()
                {
                    IsSuccess = true,
                    Data = service,
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
