using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string name { get; set; }
    }
}