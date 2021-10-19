using Microsoft.EntityFrameworkCore;
using NTPWebShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTPWebShop.Data
{
    public class NTPWebShopDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public NTPWebShopDBContext(DbContextOptions<NTPWebShopDBContext> options)
           : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            //modelBuilder.Entity<Product>().HasData(
            //    new Product()
            //    {

            //        Name = "Laptop",
            //        InStock = 5,
            //        IsActive = true,
            //        Unit = 3,
            //        CreatedOn = DateTime.Now
            //    },

            //    new Product()
            //    {
            //        Name = "LED",
            //        InStock = 15,
            //        IsActive = true,
            //        Unit = 10,
            //        CreatedOn = DateTime.Now
            //    },
            //    new Product()
            //    {
            //        Name = "KeyBoard",
            //        InStock = 5,
            //        IsActive = true,
            //        Unit = 2,
            //        CreatedOn = DateTime.Now
            //    }
            //    );

            //base.OnModelCreating(modelBuilder);
        }
    }
}
