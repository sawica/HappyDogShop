using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDogShop2.Models
{
    public enum Status
    {
        Otwarte, Przyjęte, Zapłacone, Wysłane, Dostarczone
    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        [ForeignKey("User"), Required(ErrorMessage = "To pole jest wymagane")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Suma"), DefaultValue(0)]
        public decimal AmountPaid { get; set; }

        [DisplayFormat(DataFormatString = "Otwarte"), DefaultValue(0)]
        public Status Status { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), DataType(DataType.DateTime), Display(Name = "Data zamówienia")]
        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<CartItem> CartItems { get; set; }
    }
}