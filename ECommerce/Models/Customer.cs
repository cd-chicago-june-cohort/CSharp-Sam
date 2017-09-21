using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public abstract class BaseEntity {}
    public class Customer : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime created_at {get;set;}
        public List<Purchase> purchases { get; set; }
 
        public Customer()
        {
            created_at = DateTime.Now;
            purchases = new List<Purchase>();
        }
    }
}