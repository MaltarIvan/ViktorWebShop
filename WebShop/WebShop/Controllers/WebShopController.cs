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
            List<Product> products = await _repository.GetAllProductsAsync();
            WebShopVM webShopVM = new WebShopVM(products);
            // ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            return View(webShopVM);
        }

        public async Task<IActionResult> AddProductToCart(Guid productID)
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            await shoppingCartActions.AddProductAsync(productID);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveProductFromCart(Guid cartItemID)
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            await shoppingCartActions.RemoveCartItem(cartItemID);
            return RedirectToAction("ShoppingCart");
        }

        public IActionResult ShoppingCart()
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM(shoppingCartActions.GetShoppingCart());
            return View(shoppingCartVM);
        }
    }
}