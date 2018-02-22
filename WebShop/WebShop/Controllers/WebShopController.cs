using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using WebShop.Core;
using WebShop.Models.Shared;
using WebShop.Utilities;
using Microsoft.AspNetCore.Http;
using WebShop.Models.WebShop;

namespace WebShop.Controllers
{
    public class WebShopController : Controller
    {
        private readonly IWebShopRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public WebShopController(IWebShopRepository repository, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public async Task<IActionResult> Index()
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            List<Product> products = await _repository.GetAllProductsAsync();
            List<ProductVM> productsVM = new List<ProductVM>();
            foreach (var item in products)
            {
                productsVM.Add(new ProductVM(item, shoppingCartActions.Contains(item)));
            }
            WebShopVM webShopVM = new WebShopVM(productsVM);
            return View(webShopVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(Guid productID)
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            int productCount = await shoppingCartActions.AddProductAsync(productID);
            double totalPrice = shoppingCartActions.TotalPrice();
            double cartItemPrice = shoppingCartActions.CartItemPrice(productID);
            return Json(new { TotalPrice =  totalPrice, CartItemPrice = cartItemPrice, ProductCount = productCount, NumberOfCartItems = shoppingCartActions.NumberOfCartItems() });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProductFromCart(Guid cartItemID)
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            int productCount = await shoppingCartActions.RemoveCartItem(cartItemID);
            double totalPrice = shoppingCartActions.TotalPrice();
            double cartItemPrice = 0;
            if (productCount != 0)
            {
                cartItemPrice = shoppingCartActions.CartItemPrice(cartItemID);
            }
            return Json(new { TotalPrice = totalPrice, CartItemPrice = cartItemPrice, ProductCount = productCount, NumberOfCartItems = shoppingCartActions.NumberOfCartItems() });
        }

        public async Task<IActionResult> DeleteProductFromCart(Guid cartItemID)
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            await shoppingCartActions.DeleteCartItem(cartItemID);
            return Json(new { TotalPrice = shoppingCartActions.TotalPrice(), NumberOfCartItems = shoppingCartActions.NumberOfCartItems() });
        }

        public IActionResult ShoppingCart()
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM(shoppingCartActions.GetShoppingCart());
            return View(shoppingCartVM);
        }

        [HttpGet]
        public IActionResult NumberOfCartItems()
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            return Json(shoppingCartActions.NumberOfCartItems());
        }
    }
}