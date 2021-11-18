using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappyDogShop2.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required, StringLength(50, MinimumLength = 3), Display(Name = "First name")]
        public string First_name { get; set; }
        [Required, StringLength(50, MinimumLength = 3), Display(Name = "Last name")]
        public string Last_name { get; set; }
        [Required, StringLength(10, MinimumLength = 6)]
        public string Username { get; set; }
        [Required, StringLength(10, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required, DataType(DataType.PhoneNumber), Display(Name = "Phone number")]
        //public string Phone_number { get; set; }
        [DefaultValue(false), Display(Name = "Is admin")]
        public bool Is_admin { get; set; }
    }
}