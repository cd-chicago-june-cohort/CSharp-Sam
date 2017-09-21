using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ECommerce.Models
{

    public class Product : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }

        public string description { get; set; }
        public int quantity { get; set; }
        public DateTime created_at {get;set;}
        public List<Purchase> orders { get; set; }
 
        public Product()
        {
            created_at = DateTime.Now;
            orders = new List<Purchase>();
        }
    }
}