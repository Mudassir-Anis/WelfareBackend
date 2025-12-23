using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlifSani.Models
{
    public class FinalizedModel
    {
        public string TransactionID { get; set; }
        public string Customer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class test {

        public string amount { get; set; }

        public string currency { get; set; }

        public string OrderID { get; set; }
      

    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
   

    public class Payer
    {
        public string Information { get; set; }
    }

    public class FinalizedResponseModel
    {
        public TransactionFinalized Transaction { get; set; }
    }

    public class TransactionFinalized
    {
        public string ResponseCode { get; set; }
        public string ResponseClass { get; set; }
        public string ResponseDescription { get; set; }
        public string ResponseClassDescription { get; set; }
        public string Language { get; set; }
        public string ApprovalCode { get; set; }
        public string Account { get; set; }
        public Balance Balance { get; set; }
        public string OrderID { get; set; }
        public Amount Amount { get; set; }
        public Fees Fees { get; set; }
        public string CardNumber { get; set; }
        public Payer Payer { get; set; }
        public string CardToken { get; set; }
        public string CardBrand { get; set; }
        public string CardType { get; set; }
        public string IsWalletUsed { get; set; }
        public string IsCaptured { get; set; }
        public string UniqueID { get; set; }
    }


}