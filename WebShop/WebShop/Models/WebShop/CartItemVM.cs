using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;

namespace WebShop.Models.WebShop
{
    public class CartItemVM
    {
        public Guid CartItemID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImageName { get; set; }

        public CartItemVM(CartItem cartItem)
        {
            CartItemID = cartItem.CartItemID;
            ProductID = cartItem.ProductID;
            ProductName = cartItem.Product.Name;
            Quantity = cartItem.Quantity;
            ImageName = cartItem.Product.ImageName;
        }
    }
}
