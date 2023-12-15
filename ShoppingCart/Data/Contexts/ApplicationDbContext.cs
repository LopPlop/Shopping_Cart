using ShoppingCart.Models;

namespace ShoppingCart.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }
    }
}
