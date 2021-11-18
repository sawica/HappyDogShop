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
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Range(1, 99)] 
        public int Value_in_percent { get; set; }


        public virtual ICollection<Product> Products { get; set; } 
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}