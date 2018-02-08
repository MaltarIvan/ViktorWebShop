using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;

namespace WebShop.Models.WebShop
{
    public class WebShopVM
    {
        public List<ProductVM> ProductsVM;

        public WebShopVM(List<Product> products)
        {
            ProductsVM = new List<ProductVM>();
            foreach (var item in products)
            {
                ProductsVM.Add(new ProductVM(item));
            }
        }
    }
}
