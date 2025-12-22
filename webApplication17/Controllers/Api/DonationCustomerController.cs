using AlifSani.Models;
using AlifSani.Models.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;


namespace AlifSani.Controllers.Api
{
    public class DonationCustomerController : ApiController
    {
        //[BasicAuthentication]
        [HttpPost]
        public PaymentResponse POST([FromBody] DonateCustomerInfo DonateCustomerInfo)
        {
            try
            {
                string UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                string password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                string CustomerName = System.Configuration.ConfigurationManager.AppSettings["CustomerName"];
                string Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];


                string URL = System.Configuration.ConfigurationManager.AppSettings["Url"];
                string ReturnPath = System.Configuration.ConfigurationManager.AppSettings["ReturnPath"];

                string TransactionHint = System.Configuration.ConfigurationManager.AppSettings["TransactionHint"];
                string OrderName = "Sadaqa";

                var v=new Entities().DonationItems.Where(x => x.Id == DonateCustomerInfo.ItemId).FirstOrDefault();

                if (v!=null)
                {
                    OrderName = v.Title;
                }

                

                

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //string URL = "https://demo-ipg.ctdev.comtrust.ae:2443";
                string InputJSON = "{\"Registration\":"
                    + "{\"Customer\":\""+CustomerName+"\"," +
                    "\"Channel\":\"Web\"," +
                    "\"Amount\":\""+ DonateCustomerInfo.Amount + "\"," +
                    "\"Currency\":\""+Currency+"\"," +
                    "\"OrderID\":null," +
                    "\"OrderName\":\""+OrderName+"\"," +
                    "\"OrderInfo\":null," +
                    "\"TransactionHint\":\""+TransactionHint+"\"," +
                    "\"UserName\":\""+UserName+"\"," +
                    "\"Password\":\""+password+"\"," +
                    "\"ReturnPath\":\""+ReturnPath+"\"}}";

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

                PaymentRegResponseModel myDeserializedClass = new PaymentRegResponseModel();
                //PaymentResponse myDeserializedClass = new PaymentResponse();

                if (!string.IsNullOrEmpty(result))
                {

                    myDeserializedClass = JsonConvert.DeserializeObject<PaymentRegResponseModel>(result);
                    //myDeserializedClass = JsonConvert.DeserializeObject<PaymentResponse>(result);
                    DonateCustomerInfo.Zip = "Pending";
                    DonateCustomerInfo.Status = false;
                    DonateCustomerInfo.ExtraInformation = myDeserializedClass.Transaction.TransactionID.ToString();


                    var news = Models.ApiDBHelper.DonateCustomerInfo(DonateCustomerInfo);

                    return new PaymentResponse()
                    {
                        IsSuccess = true,
                        Data = news,
                        Message = "Success",
                        TransactionId = myDeserializedClass.Transaction.TransactionID ?? "",
                        Url = myDeserializedClass.Transaction.PaymentPortal ?? ""

                    };
                }
                else 
                {
                    return new PaymentResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message ="Error",
                        TransactionId = "",
                        Url = "",
                    };
                }

                    response.Close();
                    loResponseStream.Close();

                   


               


            }
            catch (Exception ex)
            {
                return new PaymentResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message,
                    TransactionId = "",
                    Url = "",
                };

            }
            
        }
    }
}
