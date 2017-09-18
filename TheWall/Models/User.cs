using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string First { get; set; }
        [Required]
        [MinLength(2)]
        public string Last { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        public string Confirm {get; set;}
    }
}