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
    public class DonationCategoryController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET()
        {
            try
            {
                var news = Models.ApiDBHelper.DonationCategoriesRealtion();

                return new WebApiResponse()
                {
                    IsSuccess = true,
                    Data = news,
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
