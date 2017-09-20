using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Weddings.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        public int id { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public List<Wedding> weddings { get; set; }

        public List<Guest> events { get; set; }
 
        public User()
        {
            weddings = new List<Wedding>();
            events = new List<Guest>();
        }
    }
}