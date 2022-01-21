using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HappyDogShop2.Models
{
public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"),Display(Name = "Nazwa")]
        public string Name { get; set; }
        [DefaultValue(false), Display(Name = "Ukryty")]
        public bool IsHidden { get; set; }
        
        [ForeignKey("Parent"), Display(Name = "Rodzic")]
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        
        [ForeignKey("Sale"), Display(Name = "Promocja")]
        public int? SaleId { get; set; }
        public virtual Sale Sale { get; set; }
        
        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}