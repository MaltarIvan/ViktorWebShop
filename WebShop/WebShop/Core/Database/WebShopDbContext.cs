using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core.Database
{
    public class WebShopDbContext : DbContext
    {
        public WebShopDbContext(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<ShoppingCart> ShoppingCarts { get; set; }
        public IDbSet<CartItem> CartItems { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<PromoCode> PromoCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.DateAdded).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ImageName).IsRequired();

            modelBuilder.Entity<ShoppingCart>().HasKey(s => s.ShoppingCartID);
            modelBuilder.Entity<ShoppingCart>().HasMany(s => s.CartItems).WithRequired(c => c.ShoppingCart);
            modelBuilder.Entity<ShoppingCart>().Property(s => s.DateCreated).IsRequired();

            modelBuilder.Entity<CartItem>().HasKey(c => c.CartItemID);
            modelBuilder.Entity<CartItem>().Property(c => c.ShoppingCartID).IsRequired();
            modelBuilder.Entity<CartItem>().HasRequired(c => c.ShoppingCart).WithMany(s => s.CartItems);
            modelBuilder.Entity<CartItem>().Property(c => c.Quantity).IsRequired();
            modelBuilder.Entity<CartItem>().Property(c => c.DateAdded).IsRequired();
            modelBuilder.Entity<CartItem>().Property(c => c.ProductID).IsRequired();
            modelBuilder.Entity<CartItem>().HasRequired(c => c.Product);

            modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
            modelBuilder.Entity<Order>().Property(o => o.ShoppingCartID).IsRequired();
            modelBuilder.Entity<Order>().HasRequired(o => o.ShoppingCart);
            modelBuilder.Entity<Order>().Property(o => o.Name).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Surname).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.StreetAdress).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.City).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.PostalCode).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Country).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Email).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.PhoneNumber).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Details).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.PromoCode).IsRequired();
        }
    }
}
