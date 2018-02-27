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

        public async Task<CartItem> RemoveCartItemAsync(Guid cartItemID)
        {
            CartItem cartItem = await GetCartItemAsync(cartItemID);
            _context.CartItems.Remove(cartItem);
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
            return await _context.Orders.Include(o => o.PromoCode).Include(o => o.ShoppingCart.CartItems.Select(c => c.Product)).Where(o => o.IsCompleted).ToListAsync();
        }

        public async Task<Order> GetOrderAsync(Guid orderID)
        {
            return await _context.Orders.Include(o => o.ShoppingCart.CartItems.Select(c => c.Product)).Include(o => o.PromoCode).SingleOrDefaultAsync(o => o.OrderID == orderID);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            /*
            _context.Orders.Attach(order);
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            */
            var ord = await _context.Orders.Include(o => o.PromoCode).FirstAsync(o => o.OrderID == order.OrderID);
            ord.OrderID = order.OrderID;
            ord.ShoppingCartID = order.ShoppingCartID;
            ord.ShoppingCart = order.ShoppingCart;
            ord.Name = order.Name;
            ord.Surname = order.Surname;
            ord.StreetAdress1 = order.StreetAdress1;
            ord.StreetAdress2 = order.StreetAdress2;
            ord.City = order.City;
            ord.PostalCode = order.PostalCode;
            ord.Country = order.Country;
            ord.Email = order.Email;
            ord.MobilePhoneNumber = order.MobilePhoneNumber;
            ord.PhoneNumber = order.PhoneNumber;
            ord.PromoCode = order.PromoCode;
            ord.IsCompleted = order.IsCompleted;
            ord.IsDelivered = order.IsDelivered;
            ord.PaymentMethod = order.PaymentMethod;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetDeliveredOrdersAsync()
        {
            return await _context.Orders.Include(o => o.PromoCode).Include(o => o.ShoppingCart.CartItems.Select(c => c.Product)).Where(o => o.IsDelivered).ToListAsync();
        }

        public async Task<Order> DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<PromoCode> AddPromoCodeAsync(PromoCode promoCode)
        {
            _context.PromoCodes.Add(promoCode);
            await _context.SaveChangesAsync();
            return promoCode;
        }

        public async Task<PromoCode> GetPromoCodeAsync(Guid promoCodeID)
        {
            return await _context.PromoCodes.SingleOrDefaultAsync(p => p.PromoCodeID == promoCodeID);
        }

        public async Task<PromoCode> GetPromoCodeAsync(string code)
        {
            return await _context.PromoCodes.SingleOrDefaultAsync(p => p.Code == code);
        }

        public async Task<PromoCode> UpdatePromoCodeAsync(PromoCode promoCode)
        {
            _context.PromoCodes.Attach(promoCode);
            _context.Entry(promoCode).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return promoCode;
        }

        public async Task<PromoCode> DeletePromoCodeAsync(PromoCode promoCode)
        {
            _context.PromoCodes.Remove(promoCode);
            await _context.SaveChangesAsync();
            return promoCode;
        }

        public async Task<List<PromoCode>> GetAllPromoCodesAsync()
        {
            return await _context.PromoCodes.ToListAsync();
        }

        public async Task<Picture> AddPictureAsync(Picture picture)
        {
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();
            return picture;
        }

        public async Task<Picture> GetPictureAsync(Guid pictureID)
        {
            return await _context.Pictures.FirstOrDefaultAsync(p => p.PictureID == pictureID);
        }

        public async Task<List<Picture>> GetAllPicturesAsync()
        {
            return await _context.Pictures.OrderBy(p => p.DateAdded).ToListAsync();
        }

        public async Task<Picture> DeletePictureAsync(Guid pictureID)
        {
            Picture picture = await GetPictureAsync(pictureID);
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
            return picture;
        }

        public async Task<int> DeleteUnusedShoppingCartsAndOrdersAsync()
        {
            int count = 0;
            List<ShoppingCart> unusedShoppingCarts = await _context.ShoppingCarts.Where(s => ((int)DbFunctions.DiffDays(s.DateCreated, DateTime.Now)) > 1).ToListAsync();
            foreach (var item in unusedShoppingCarts)
            {
                if (_context.Orders.All(o => o.ShoppingCartID != item.ShoppingCartID))
                {
                    _context.ShoppingCarts.Remove(item);
                    count++;
                }
            }
            List<Order> unfinishedOrders = await _context.Orders.Include(o => o.ShoppingCart).Where(o => ((int)DbFunctions.DiffDays(o.DateCreated, DateTime.Now)) > 1).ToListAsync();
            foreach (var item in unfinishedOrders)
            {
                if (item.IsCompleted || item.IsDelivered)
                {
                    continue;
                }
                else
                {
                    _context.ShoppingCarts.Remove(item.ShoppingCart);
                    count++;
                }
            }
            await _context.SaveChangesAsync();
            return count;
        }
    }
}
