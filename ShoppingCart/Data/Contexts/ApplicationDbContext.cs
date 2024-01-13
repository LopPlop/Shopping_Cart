using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Cart> Cart => Set<Cart>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) 
            : base(option) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };
            Role salesmanRole = new Role { Id = 3, Name = "salesman" };

            User adminUser = new User { Id = 1, Email = "admin@gmail.ru", Password = "123456", RoleId = adminRole.Id };

            Category gpu = new Category { Id = 1, Name = "GPU", Description = "-" };
            Category cpu = new Category { Id = 2, Name = "CPU", Description = "-" };
            Category motherboard = new Category { Id = 3, Name = "Motherboard", Description = "-" };

            Product firstGPU = new Product { Id = 1, Name = "Rtx 3060", Available = 12, CategoryId = 1, Image = "3060palit.jpg", IsInCart = false, LongDescription = "-", Price = 30000, ShortDescription = "-" };
            Product secondGPU = new Product { Id = 2, Name = "Rtx 3060ti", Available = 15, CategoryId = 1, Image = "3060ti.jpg", IsInCart = false, LongDescription = "-", Price = 31000, ShortDescription = "-" };

            Product firstCPU = new Product { Id = 3, Name = "Intel Core i5-12500", Available = 15, CategoryId = 2, Image = "i5-12500.jpg", IsInCart = false, LongDescription = "-", Price = 12000, ShortDescription = "-" };
            Product secondCPU = new Product { Id = 4, Name = "Intel Core i7-10700", Available = 15, CategoryId = 2, Image = "i7-10700.jpg", IsInCart = false, LongDescription = "-", Price = 25000, ShortDescription = "-" };


            Product firstMotherboard = new Product { Id = 5, Name = "Asus prime z790", Available = 15, CategoryId = 3, Image = "Asus prime z790.jpg", IsInCart = false, LongDescription = "-", Price = 17000, ShortDescription = "-" };
            Product secondMotherboard = new Product { Id = 6, Name = "Asus_Prime_b760-a", Available = 15, CategoryId = 3, Image = "Asus_Prime_b760-a.jpg", IsInCart = false, LongDescription = "-", Price = 7000, ShortDescription = "-" };


            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, salesmanRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<Category>().HasData(new Category[] { gpu, cpu, motherboard });
            modelBuilder.Entity<Product>().HasData(new Product[] { firstCPU, secondCPU, firstGPU, secondGPU, firstMotherboard, secondMotherboard });
            base.OnModelCreating(modelBuilder);
        }
    }
}
