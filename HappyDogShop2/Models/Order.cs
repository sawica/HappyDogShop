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
        public int OrderId { get; set; }
        
        [ForeignKey("User"), Required(ErrorMessage = "To pole jest wymagane")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Adres")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Miasto")]
        public string City { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Kraj")]
        public string Country { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), DataType(DataType.PostalCode), Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Suma")]
        public decimal AmountPaid { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), DataType(DataType.PhoneNumber), Display(Name = "Numer telefonu")]
        public string Phone_number { get; set; }
        [DisplayFormat(DataFormatString = "Przyjęte")]
        public Status Status { get; set; }
        
        public List<CartItem> CartItem { get; set; }
    }
}