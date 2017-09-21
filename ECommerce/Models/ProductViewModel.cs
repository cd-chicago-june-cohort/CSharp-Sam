using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class ProductViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string name { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string image { get; set; }

        [Required]
        public string description { get; set; }
        
        [Required(ErrorMessage = "Quantity input is required.")]
        [Range(0,10000000000000000000, ErrorMessage = "Value cannot be zero or lower.")]
        public int quantity { get; set; }
    }
}