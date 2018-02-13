﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using WebShop.Core;
using WebShop.Models.WebShop;
using WebShop.Utilities;
using Microsoft.AspNetCore.Http;

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

        public IActionResult ShoppingCart()
        {
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            List<CartItem> cartItems = shoppingCartActions.GetCartItems();
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM(cartItems);
            return View(shoppingCartVM);
        }
    }
}