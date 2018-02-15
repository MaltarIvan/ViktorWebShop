using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core.Repositories
{
    public interface IWebShopRepository
    {
        Task<Product> GetProductAsync(Guid productID);
        Task<Product> AddProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> UpdateProductAsync(Product product);
        Task<Product> DeleteProductAsync(Guid productID);
        Task<Product> DeleteProductAsync(Product product);

        Task<ShoppingCart> GetShoppingCartAsync(Guid cartID);
        Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart);

        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem> GetCartItemAsync(Guid cartItemID);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);

        Task<Order> AddOrderAsync(Order order);
        Task<List<Order>> GetAllCompletedOrdersAsync();
        Task<List<Order>> GetDeliveredOrdersAsync();
        Task<Order> GetOrderAsync(Guid orderID);
        Task<Order> UpdateOrderAsync(Order order);
        Task<Order> DeleteOrderAsync(Order order);
    }
}
