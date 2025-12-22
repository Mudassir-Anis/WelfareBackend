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
    public class BeneficiaryAccountController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET()
        {
            try
            {
                var BeneficiaryAccounts = Models.ApiDBHelper.BeneficiaryAccounts();
                return new WebApiResponse()
                {
                    IsSuccess = true,
                    Data = BeneficiaryAccounts,
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
