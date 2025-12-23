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
    public class AuditReportController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET()
        {
            try
            {
                var AuditReports = Models.ApiDBHelper.AuditReports().ToList().ConvertAll(row => {

                    return new AuditReport()
                    {
                        Id = row.Id,
                        Title=row.Title,
                        FileName = string.IsNullOrEmpty(row.FileName) ? "" : Url.Content("~/") + "Content/Attachment/" + row.FileName,
                        Status = row.Status,
                        CreatedDate = row.CreatedDate

                    };

                });
                return new WebApiResponse()
                {
                    IsSuccess = true,
                    Data = AuditReports,
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
