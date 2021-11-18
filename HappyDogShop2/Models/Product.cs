using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappyDogShop2.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required"), StringLength(60, MinimumLength = 3)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(1, 10000)] //mozna zrobic wlasny validator
        public int Price { get; set; }
        [DefaultValue(false), Display(Name = "Is hidden")]
        public bool Is_hidden { get; set; }
        [Required(ErrorMessage = "Stock count is required"), Display(Name = "Stock count")]
        public int Stock_count { get; set; }
        
        
        [Required(ErrorMessage = "Category is required")]
        public virtual Category Category { get; set; }
        public virtual Sale Sale { get; set; }
    }
}