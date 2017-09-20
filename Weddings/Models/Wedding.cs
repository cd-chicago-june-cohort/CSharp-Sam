using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Weddings.Models
{
    public class Wedding : BaseEntity
    {
        public int id { get; set; }
        public string wedder1 { get; set; }
        public string wedder2 { get; set; }
        public DateTime date { get; set; }
        public string address { get; set; }
        //user refers to creator
        public int userId { get; set; }
        public User user {get;set;}
        public List<Guest> guests { get; set; }
        public Wedding()
        {
            guests = new List<Guest>();
        }
    }
}