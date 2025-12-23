using AlifSani.Models.EntityFramework;
using AlifSani.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Web;

namespace AlifSani.Controllers.Api
{
    public class ReturnPathaymentController : ApiController
    {
        
        [HttpPost]
        public HttpResponseMessage POST()
        {
            try
            {
                var sHTML = "<h1>Thank you for Payment ...</h1>\r\n   " +
                    " <p>Payment Proceed Success Fully.....</p>\r\n   " +
                    " <p><a href=\"https://mujaddidalfsani.com/\">Go to Our Web site</a></p>";

                var fHTML = "<h1>Payment Failed</h1>\r\n    " +
                    "<p> Payment can't submit .....</p>\r\n    " +
                    "<p>Please try again <a href=\"https://mujaddidalfsani.com/\">Click Here</a></p>";

                var html = "";

                var data=HttpContext.Current.Request.Form["TransactionID"].ToString();


                using (var client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes("admin:admin123");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


                    client.BaseAddress = new Uri("https://dev.mujaddidalfsani.com");
                    //HTTP GET
                    var responseTask = client.GetAsync("/api/FinalizedStatus?TransactionID=" + data);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<FinalizedCallResponseModel>();
                        readTask.Wait();

                        var res = readTask.Result;

                        if (res.Data=="0")
                        {
                            html = sHTML;
                        }
                        else
                        {
                            html = fHTML;
                        }


                    }
                    else 
                    {

                        html = fHTML;
                    }

                    var response = new HttpResponseMessage();
                    response.Content = new StringContent(html);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                    return response;
                }


                

                





                


            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage();
                response.Content = new StringContent("<div>"+ex.Message+"</div>");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;


              

            }

        }

    }
}
