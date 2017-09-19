using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class Transaction : BaseEntity
    {
        public Transaction(){
            created_at = DateTime.Now;
        }
        public int id { get; set; }
        public double amount { get; set; }

        public DateTime created_at { get; set; }

        public int userId { get; set; }
        public User user {get;set;}

    }
}