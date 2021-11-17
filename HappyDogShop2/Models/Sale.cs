using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappyDogShop2.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        [Required(ErrorMessage = "Start date is required"), DataType(DataType.Date), Display(Name = "Start date")]
        public DateTime Start_date { get; set; }
        [Required(ErrorMessage = "End date is required"), DataType(DataType.Date), Display(Name = "End date")]
        public DateTime End_date { get; set; }

        //type
        //value moze zrobic to z juz gotowymi rzeczami
    }
}