using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;

namespace WebShop.Models.Shared
{
    public class ShoppingCartVM
    {
        public List<CartItemVM> CartItemsVM { get; set; }
        public double TotalPrice { get; set; }

        public ShoppingCartVM(ShoppingCart shoppingCart)
        {
            TotalPrice = shoppingCart.TotalPrice;
            CartItemsVM = new List<CartItemVM>();
            foreach (var item in shoppingCart.CartItems)
            {
                CartItemsVM.Add(new CartItemVM(item));
            }
        }
    }
}
