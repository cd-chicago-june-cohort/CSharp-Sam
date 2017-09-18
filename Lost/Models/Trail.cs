using System.ComponentModel.DataAnnotations;
namespace Lost.Models
{
    public abstract class BaseEntity {}
    public class Trail : BaseEntity
    {
        [Key]
        public long Id { get; set; }
 
        [Required]
        public string Name { get; set; }
 
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public double Length { get; set; }
 
        [Required]
        public int Elevation { get; set; }

        [Required]
        [Range(-180.0, 180.0)]
        public double Longitude { get; set; }
        [Required]
        [Range(-90.0, 90.0)]
        public double Latitude { get; set; }
    }
}
