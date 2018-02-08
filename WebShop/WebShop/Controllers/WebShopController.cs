using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using WebShop.Core;
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
            return View(webShopVM);
        }
    }
}