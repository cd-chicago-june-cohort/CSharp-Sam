using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class PurchaseViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Customer must be chosen for an order")]
        public int customerId { get; set; }

        [Required(ErrorMessage = "Product must be chosen for an order")]
        public int productId { get; set; }

        [Required(ErrorMessage = "Quantity must be chosen for an order")]
        public int quantity { get; set; }

    }
}