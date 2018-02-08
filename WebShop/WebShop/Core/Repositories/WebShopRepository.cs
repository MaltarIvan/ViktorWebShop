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
        private WebShopDbContext Context;

        public WebShopRepository(WebShopDbContext context)
        {
            Context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            Context.Products.Add(product);
            await Context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Guid productID)
        {
            Product product = await GetProductAsync(productID);
            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Product product)
        {
            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await Context.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(Guid productID)
        {
            return await Context.Products.SingleOrDefaultAsync(p => p.ProductID == productID);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            Context.Products.Attach(product);
            Context.Entry(product).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return product;
        }
    }
}
