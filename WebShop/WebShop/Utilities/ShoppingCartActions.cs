using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;
using WebShop.Core.Repositories;

namespace WebShop.Utilities
{
    public class ShoppingCartActions
    {
        private Guid _shoppingCartID;
        private ShoppingCart _shoppingCart;
        private readonly IWebShopRepository _repository;
        private const string CartSessionKey = "ShoppingCartID";
        private ISession _session;

        public ShoppingCartActions(ISession session, IWebShopRepository repository)
        {
            _session = session;
            _repository = repository;

            string id = _session.GetString(CartSessionKey);

            if (id == null)
            {
                id = Guid.NewGuid().ToString();
                _session.SetString(CartSessionKey, id);
            }

            _shoppingCartID = new Guid(id);
            _shoppingCart = _repository.GetShoppingCartAsync(_shoppingCartID).Result;
            if (_shoppingCart == null)
            {
                _shoppingCart = new ShoppingCart(_shoppingCartID);
                _repository.AddShoppingCartAsync(_shoppingCart).Wait();
            }
        }

        public bool Contains(Product product)
        {
            return _shoppingCart.CartItems.Any(c => c.ProductID == product.ProductID);
        }

        public double TotalPrice()
        {
            return _shoppingCart.TotalPrice;
        }

        public int NumberOfCartItems()
        {
            int num = 0;
            foreach (var item in _shoppingCart.CartItems)
            {
                num += item.Quantity;
            }
            return num;
        }

        public double CartItemPrice(Guid productID)
        {
            CartItem cartItem = _shoppingCart.CartItems.First(c => c.ProductID == productID || c.CartItemID == productID);
            return cartItem.Quantity * cartItem.PricePerItem;
        }

        public async Task<int> AddProductAsync(Guid productID)
        {
            CartItem cartItem = _shoppingCart.CartItems.FirstOrDefault(c => c.ProductID == productID);
            if (cartItem == null)
            {
                Product product = await _repository.GetProductAsync(productID);
                cartItem = new CartItem(_shoppingCart, product, 1);
                await _repository.AddCartItemAsync(cartItem);
                return 1;
            }
            else
            {
                cartItem.Quantity++;
                await _repository.UpdateCartItemAsync(cartItem);
                return cartItem.Quantity;
            }
        }

        public async Task<int> RemoveCartItem(Guid cartItemID)
        {
            CartItem cartItem = _shoppingCart.CartItems.FirstOrDefault(c => c.CartItemID == cartItemID);
            if (cartItem.Quantity == 1)
            {
                await DeleteCartItem(cartItemID);
                return 0;
            }
            else
            {
                cartItem.Quantity--;
                await _repository.UpdateCartItemAsync(cartItem);
                return cartItem.Quantity;
            }
        }

        internal async Task DeleteCartItem(Guid cartItemID)
        {
            await _repository.RemoveCartItemAsync(cartItemID);
        }

        internal ShoppingCart GetShoppingCart()
        {
            return _shoppingCart;
        }

        internal List<CartItem> GetCartItems()
        {
            return _shoppingCart.CartItems;
        }

        internal async Task EmptyTheCartAsync()
        {
            List<CartItem> cartItems = new List<CartItem>(_shoppingCart.CartItems);
            foreach (var item in cartItems)
            {
                await _repository.RemoveCartItemAsync(item.CartItemID);
            }
        }
    }
}
