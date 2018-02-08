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
    }
}
