using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlifSani.Models
{
    public class FinalizedCallResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
    }
}