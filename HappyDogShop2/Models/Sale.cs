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
        [Required(ErrorMessage = "To pole jest wymagane"), DataType(DataType.Date), Display(Name = "Data rozpoczęcia")]
        public DateTime Start_date { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), DataType(DataType.Date), Display(Name = "Data zakończenia")]
        public DateTime End_date { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Range(1, 99), Display(Name = "Wartość w %")] 
        public int Value_in_percent { get; set; }


        public virtual ICollection<Product> Products { get; set; } 
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}