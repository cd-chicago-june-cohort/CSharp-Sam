using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Bank.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        public User(){
            Random rand = new Random();
            var balanceNum = rand.Next(0,11);
            balance = balanceNum * 100;
            transactions = new List<Transaction>();
        }
        public int id { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public double balance {get;set;}
        public List<Transaction> transactions {get;set;}
    }
}