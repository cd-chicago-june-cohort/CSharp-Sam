using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string first { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string last { get; set; }
 
        [Required]
        [EmailAddress]
        public string email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }
 
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        public string confirm { get; set; }
    }
}