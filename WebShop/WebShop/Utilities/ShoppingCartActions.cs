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

        public async Task AddProductAsync(Guid productID)
        {
            CartItem cartItem = _shoppingCart.CartItems.FirstOrDefault(c => c.ProductID == productID);
            if (cartItem == null)
            {
                Product product = await _repository.GetProductAsync(productID);
                cartItem = new CartItem(_shoppingCart, product, 1);
                await _repository.AddCartItemAsync(cartItem);
            }
            else
            {
                cartItem.Quantity++;
                await _repository.UpdateCartItemAsync(cartItem);
            }
        }

        internal ShoppingCart GetShoppingCart()
        {
            return _shoppingCart;
        }

        internal List<CartItem> GetCartItems()
        {
            return _shoppingCart.CartItems;
        }
    }
}
