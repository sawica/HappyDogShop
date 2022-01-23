using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDogShop2.Models
{
    public class CartItem
    {
        [Key]
        public int ItemId { get; set; }  
        [Required(ErrorMessage = "Wybierz między 1 a 15"), Display(Name = "Ilość"), Range(1, 15)]
        public int Quantity  { get; set; }
        
        [ForeignKey("Product"), Display(Name = "Produkt")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        [ForeignKey("Order"), Display(Name = "Zamówienie")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        
    }
}