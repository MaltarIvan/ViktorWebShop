using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core.Database;

namespace WebShop.Core.Repositories
{
    public class WebShopRepository : IWebShopRepository
    {
        private WebShopDbContext _context;

        public WebShopRepository(WebShopDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Guid productID)
        {
            Product product = await GetProductAsync(productID);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(Guid productID)
        {
            return await _context.Products.SingleAsync(p => p.ProductID == productID);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(Guid cartID)
        {
            return await _context.ShoppingCarts.Include(s => s.CartItems.Select(c => c.Product)).SingleOrDefaultAsync(s => s.ShoppingCartID == cartID);
        }

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Attach(shoppingCart);
            _context.Entry(shoppingCart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> GetCartItemAsync(Guid cartItemID)
        {
            return await _context.CartItems.SingleAsync(c => c.CartItemID == cartItemID);
        }

        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Attach(cartItem);
            _context.Entry(cartItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllCompletedOrdersAsync()
        {
            return await _context.Orders.Where(o => o.Completed).ToListAsync();
        }

        public async Task<Order> GetOrderAsync(Guid orderID)
        {
            return await _context.Orders.Include(o => o.ShoppingCart.CartItems.Select(c => c.Product)).SingleOrDefaultAsync(o => o.OrderID == orderID);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Orders.Attach(order);
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetDeliveredOrdersAsync()
        {
            return await _context.Orders.Where(o => o.Delivered).ToListAsync();
        }

        public async Task<Order> DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
