using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;

namespace WebShop.Models.WebShop
{
    public class ShoppingCartVM
    {
        public List<CartItemVM> CartItemsVM;

        public ShoppingCartVM(List<CartItem> cartItems)
        {
            CartItemsVM = new List<CartItemVM>();
            foreach (var item in cartItems)
            {
                CartItemsVM.Add(new CartItemVM(item));
            }
        }
    }
}
