using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class CartItem
    {
        [Key]
        public Guid CartItemID { get; set; }
        public Guid ShoppingCartID { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public double PricePerItem { get; set; }
        public int Quantity { get; set; }

        public CartItem(ShoppingCart shoppingCart, Product product, int quantity)
        {
            CartItemID = Guid.NewGuid();
            ShoppingCartID = shoppingCart.ShoppingCartID;
            DateAdded = DateTime.Now;
            ProductID = product.ProductID;
            Product = product;
            PricePerItem = product.Price;
            Quantity = quantity;
        }

        public CartItem()
        {
        }
    }
}
