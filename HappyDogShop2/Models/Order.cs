using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDogShop2.Models
{
    public enum Status
    {
        Przyjęte, Zapłacone, Wysłane, Dostarczone
    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        [ForeignKey("User"), Required(ErrorMessage = "To pole jest wymagane")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Suma")]
        public decimal AmountPaid { get; set; }
        [DisplayFormat(DataFormatString = "Przyjęte")]
        public Status Status { get; set; }
          
        public ICollection<CartItem> CartItems { get; set; }
    }
}