using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlifSani.Models
{
    public class WebApiResponse
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
    }
    public class PaymentResponse
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }

        public string TransactionId { get; set; }

        public string Url { get; set; }
    }
}