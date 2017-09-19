using System.ComponentModel.DataAnnotations;
namespace Bank.Models
{
    public class TransactionViewModel : BaseEntity
    {

        [Required]
        public double amount { get; set; }

    }
}