using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Weddings.Models
{
    public class Guest : BaseEntity
    {
        public int id { get; set; }
        public int weddingId { get; set; }
        public Wedding wedding {get;set;}
        public int userId { get; set; }
        public User user {get;set;}
    }
}