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

        public WebShopVM(List<ProductVM> productsVM)
        {
            ProductsVM = productsVM;
        }
    }
}
