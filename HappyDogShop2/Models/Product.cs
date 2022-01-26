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
        public float Price { get; set; }
        [DefaultValue(false), Display(Name = "Ukryty")]
        public bool IsHidden { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Stan magazynowy")]
        public int StockCount { get; set; }
        
        
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Obraz ")]
        public int MediaTypeId { get; set; }
        public virtual MediaType MediaType { get; set; }
        
        [ForeignKey("Category"), Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        [ForeignKey("Sale"), Display(Name = "Promocja")]
        public int? SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}