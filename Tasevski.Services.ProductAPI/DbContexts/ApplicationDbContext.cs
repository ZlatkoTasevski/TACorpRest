using Microsoft.EntityFrameworkCore;
using Tasevski.Services.ProductAPI.Models;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace Tasevski.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Tелешки шницли во сос",
                Price = 450,
                Description = "Вкусни телешки шницли кои ќе ги обожавате. Добар апетит!",
                ImageUrl = "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/meso-snicla.jpg",
                CategoryName = "Готвени јадења"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Вкусни ароматични компири",
                Price = 250,
                Description = "Ароматични компири со рузмарин. Добар апетит!",
                ImageUrl = "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/kompiri-tava.jpg",
                CategoryName = "Готвени јадења"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Здрав оброк од спанаќ и јајца",
                Price = 180,
                Description = "Навистина брз, вкусен и здрав оброк. Добар апетит!",
                ImageUrl = "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/spanak-jajca.jpg",
                CategoryName = "Јадења по порачка"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Мешано месо во вкусен сос",
                Price = 350,
                Description = "Вкусно и сочно мешано месо. Добар апетит!",
                ImageUrl = "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/pilesko.jpg",
                CategoryName = "Entree"
            });
        }
    }
}