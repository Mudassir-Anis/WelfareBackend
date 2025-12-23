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
using static System.Net.WebRequestMethods;
//using System.Web.Mvc;

namespace AlifSani.Controllers.Api
{
    public class FinalizedStatusController : ApiController
    {
        
        //[BasicAuthentication]
        [HttpGet]
        public WebApiResponse GET(string TransactionID)
        {
            try
            {

                string UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                string password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                string CustomerName = System.Configuration.ConfigurationManager.AppSettings["CustomerName"];
                string URL = System.Configuration.ConfigurationManager.AppSettings["Url"];

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //string URL = "https://demo-ipg.ctdev.comtrust.ae:2443";
                string InputJSON = "{\"Finalization\":"
                    + "{\"TransactionID\":\"" + TransactionID + "\"," +
                    "\"Customer\":\""+ CustomerName + "\"," +
                    "\"UserName\":\""+UserName+"\"," +
                    "\"Password\":\""+password+"\"}}";

                //X509Certificate2 certificate = new X509Certificate2("E:\\Documents\\commercesdk\\CommerceSDK\\Certificates\\Demo Merchant.pfx", "Comtrust");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL));
                byte[] lbPostBuffer = ASCIIEncoding.ASCII.GetBytes(InputJSON);
                //request.ClientCertificates.Add(certificate);               
                request.UserAgent = ".NET Framework Test Client";
                request.Accept = "text/xml-standard-api";
                request.Method = "POST";
                request.ContentLength = lbPostBuffer.Length;
                request.ContentType = "application/json";
                request.Timeout = 600000;



                HttpWebResponse response;

                Stream loPostData = request.GetRequestStream();
                loPostData.Write(lbPostBuffer, 0, lbPostBuffer.Length);
                loPostData.Close();

                response = (HttpWebResponse)request.GetResponse();
                Encoding enc = Encoding.GetEncoding(1252);
                StreamReader loResponseStream = new StreamReader(response.GetResponseStream(), enc);
                string result = loResponseStream.ReadToEnd();
                loResponseStream.Close();

                FinalizedResponseModel resp = new FinalizedResponseModel();

                if (!string.IsNullOrEmpty(result))
                {

                    resp = JsonConvert.DeserializeObject<FinalizedResponseModel>(result);


                    using (Entities db = new Entities())
                    {
                        var obj = db.DonateCustomerInfoes.Where(x => x.ExtraInformation == TransactionID).FirstOrDefault();

                        if (obj != null)
                        {
                            if (resp.Transaction.ResponseCode == "0")
                            {
                                obj.Status = true;
                                obj.Zip = "Success";
                            }
                            else
                            {
                                obj.Status = false;
                                obj.Zip = "Failed";
                            }
                            db.SaveChanges();
                        }

                    }

                        


                    return new WebApiResponse()
                    {
                        IsSuccess = true,
                        Data = resp.Transaction.ResponseCode,
                        Message = resp.Transaction.ResponseDescription,

                    };

                    response.Close();
                    loResponseStream.Close();
                }
                else 
                {
                    response.Close();
                    loResponseStream.Close();

                    return new WebApiResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Failed",

                    };
                }


               

               
     
               


            }
            catch (Exception ex)
            {
                return new WebApiResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Faild",

                };

            }

        }

    }
}
