using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HappyDogShop2.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), StringLength(50, MinimumLength = 3), Display(Name = "Imię")]
        public string First_name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), StringLength(50, MinimumLength = 3), Display(Name = "Nazwisko")]
        public string Last_name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), StringLength(10, MinimumLength = 6), Display(Name = "Login")]
        public string Username { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane"), StringLength(10, MinimumLength = 6), DataType(DataType.Password), Display(Name = "Hasło")]
        public string Password { get; set; }
        [DefaultValue(false), Display(Name = "Admin")]
        public bool Is_admin { get; set; }
        
        [ForeignKey("Sale"), Display(Name = "Promocja")]
        public int? SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}