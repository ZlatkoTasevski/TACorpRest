﻿
using Microsoft.EntityFrameworkCore;
using Tasevski.Services.CouponAPI.Models;

namespace Tasevski.Services.CouponAPI.DbContexts;
public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 1,
            CouponCode = "Popust100",
            DiscountAmount = 100
        });
        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 2,
            CouponCode = "Popust100",
            DiscountAmount = 200
        });       
    }
}
