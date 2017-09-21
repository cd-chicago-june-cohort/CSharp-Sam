using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ECommerce.Models
{

    public class Purchase : BaseEntity
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public Customer customer {get;set;}
        public int productId { get; set; }
        public Product product {get;set;}
        public int quantity { get; set; }
        public DateTime created_at {get;set;}
 
        public Purchase()
        {
            created_at = DateTime.Now;
        }
    }
}