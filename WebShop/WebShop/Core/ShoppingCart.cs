using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class ShoppingCart
    {
        [Key]
        public Guid ShoppingCartID { get; set; }
        public List<CartItem> CartItems { get; set; }
        public DateTime DateCreated { get; set; }

        public ShoppingCart(Guid shoppingCartID)
        {
            ShoppingCartID = shoppingCartID;
            CartItems = new List<CartItem>();
            DateCreated = DateTime.Now;
        }

        public ShoppingCart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
