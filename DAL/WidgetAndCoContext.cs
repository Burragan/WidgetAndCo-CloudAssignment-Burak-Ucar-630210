using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using WidgetAndCo.Domain;

namespace WidgetAndCo.DAL
{
    public class WidgetAndCoContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<ProductImage> productImage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Auto generated Id values on creation
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(o => o.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>().Property(r => r.Id).ValueGeneratedOnAdd();


            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images);

        }

        public WidgetAndCoContext(DbContextOptions<WidgetAndCoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
