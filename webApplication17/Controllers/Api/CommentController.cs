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
    public class CommentController : ApiController
    {
        [BasicAuthentication]
        [HttpPost]
        
        public WebApiResponse Post([FromBody]Comment comment)
        {
            try
            {
                if (string.IsNullOrEmpty(comment.Name))
                {
                    return new WebApiResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Name is Required"
                    };
                }
                if (string.IsNullOrEmpty(comment.Email))
                {
                    return new WebApiResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Email is Required"
                    };
                }
                if (string.IsNullOrEmpty(comment.Subject))
                {
                    return new WebApiResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Subject is Required"
                    };
                }

                int res = ApiDBHelper.Comment(comment);
                return new WebApiResponse()
                {
                    IsSuccess = res==0?false:true,
                    Data = "",
                    Message = res == 0 ?"false" :"Success"
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
