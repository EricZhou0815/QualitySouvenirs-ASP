using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Souvenir> Souvenirs { get; set; }

        //Week8

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Souvenir>().ToTable("Souvenir");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");

            //Week8
       
            modelBuilder.Entity<CartItem>().ToTable("CartItem");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<OrderDetail>().HasOne(p => p.Order).WithMany(o => o.OrderDetails).OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<QualitySouvenirs.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<QualitySouvenirs.Models.ShoppingCart> ShoppingCart { get; set; }
    }
}
