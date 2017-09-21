using Microsoft.EntityFrameworkCore;
 
namespace ECommerce.Models
{
    public class ECommerceContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) { }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Purchase> purchases { get; set; }
    }
}