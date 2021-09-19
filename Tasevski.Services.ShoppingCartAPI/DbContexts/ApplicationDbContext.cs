using Microsoft.EntityFrameworkCore;
using Tasevski.Services.ShoppingCartAPI.Models;

namespace Tasevski.Services.ShoppingCartAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<CartHeader> CartHeader { get; set; }  
        public DbSet<CartDetails> CartDetails { get; set; }

    }
}
