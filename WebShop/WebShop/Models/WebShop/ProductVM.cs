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
        public bool IsInCart;

        public ProductVM(Product product, bool isInCart)
        {
            ProductID = product.ProductID;
            Name = product.Name;
            DateAdded = product.DateAdded;
            string description = product.Description.Replace(Environment.NewLine, "<br/>");
            Description = description;
            Price = product.Price;
            ImageName = product.ImageName;
            IsInCart = isInCart;
        }
    }
}
