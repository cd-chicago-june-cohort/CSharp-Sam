using System.ComponentModel.DataAnnotations;

namespace ValidForm.Models
{
    public class User
    {
        [Required]
        [MinLength(4)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User(string first, string last, string email, string age, string pw){
            FirstName = first;
            LastName = last;
            Email = email;
            Age = age;
            Password = pw;
        }
    }
}