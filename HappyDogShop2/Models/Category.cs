using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HappyDogShop2.Models
{
public class Category
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        [DefaultValue(false), Display(Name = "Is hidden")]
        public bool Is_hidden { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<Product> Products { get; set; } //????
    }
}