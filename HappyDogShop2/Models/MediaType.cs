using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappyDogShop2.Models
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Tytuł")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "To pole jest wymagane"), Display(Name = "Ścieżka")]
        public string ImagePath { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}