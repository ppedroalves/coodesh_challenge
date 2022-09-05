using Commerce.Data.Mappings;
using Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Data
{
    public class Context : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; } 

        public DbSet<Product> Products { get; set; }
        
        public DbSet<Provider> Providers { get; set; }

        public DbSet<Itens> Itens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Commerce;User ID=sa;Password=1q2w3e4r@#$");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProviderMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ItensMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
