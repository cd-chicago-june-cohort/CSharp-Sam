using System.ComponentModel.DataAnnotations;
using System;

namespace Weddings.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required]
        public string wedder1 { get; set; }
        [Required]
        public string wedder2 { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public string address { get; set; }

    }
}