using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;

namespace WebShop.Models.WebShop
{
    public class ProductVM
    {
        public Guid ProductID;
        public string Name;
        public DateTime DateAdded;
        public string Description;
        public double Price;
        public string ImageName;
        public int Quantity;

        public ProductVM(Product product)
        {
            ProductID = product.ProductID;
            Name = product.Name;
            DateAdded = product.DateAdded;
            Description = product.Description;
            Price = product.Price;
            ImageName = product.ImageName;
        }
    }
}
