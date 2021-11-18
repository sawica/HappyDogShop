using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HappyDogShop2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), StringLength(60, MinimumLength = 3), Display(Name = "Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Range(1, 10000), Display(Name = "Cena")] //mozna zrobic wlasny validator
        public int Price { get; set; }
        [DefaultValue(false), Display(Name = "Ukryty")]
        public bool Is_hidden { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Stan magazynowy")]
        public int Stock_count { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Obraz nr 1")]
        public string Image1 { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Obraz nr 2")]
        public string Image2 { get; set; }
        
        
        [ForeignKey("Category"), Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        [ForeignKey("Sale"), Display(Name = "Promocja")]
        public int? SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}