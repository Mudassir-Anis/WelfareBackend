using AlifSani.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlifSani.Models
{
    public class DonationCategoryModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public List<DonationItem> DonationItems { get; set; }

    }
}