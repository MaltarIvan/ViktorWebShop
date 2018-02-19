using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.ManageWebShop;
using System.IO;
using WebShop.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using WebShop.Core;
using Microsoft.AspNetCore.Authorization;

namespace WebShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageWebPageController : Controller
    {
        private readonly IWebShopRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ManageWebPageController(IWebShopRepository repository, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(AddNewProductVM addNewProductVM)
        {
            var validImageTypes = new string[]
            {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
            };
            if (ModelState.IsValid)
            {
                string priceStr = addNewProductVM.Price;
                priceStr = priceStr.Replace(".", ",");
                Decimal priceDec = Convert.ToDecimal(priceStr);
                bool isPriceFormat = Decimal.Round(priceDec, 2) == priceDec;
                if (!validImageTypes.Contains(addNewProductVM.Image.ContentType))
                {
                    ModelState.AddModelError("CustomError", "Please choose either a GIF, JPG or PNG image.");
                    addNewProductVM.Image = null;
                    return View(addNewProductVM);
                }
                else if (!isPriceFormat)
                {
                    ModelState.AddModelError("CustomError", "The price format is incorrect.");
                    addNewProductVM.Price = null;
                    return View(addNewProductVM);
                }
                else
                {
                    var file = addNewProductVM.Image;
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Content\\products");
                    string fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    string path = Path.Combine(uploads, fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    Product product = new Product(addNewProductVM.Name, addNewProductVM.Description, Convert.ToDouble(priceDec), fileName);
                    await _repository.AddProductAsync(product);
                    return RedirectToAction("Index");
                }
            }
            return View(addNewProductVM);
        }

        public IActionResult AddNewPromoCode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPromoCode(AddNewPromoCodeVM addNewPromoCodeVM)
        {
            if (ModelState.IsValid)
            {
                PromoCode promoCode = new PromoCode(addNewPromoCodeVM.Code, addNewPromoCodeVM.Category);
                await _repository.AddPromoCodeAsync(promoCode);
                return RedirectToAction("PromoCodes");
            }
            return View(addNewPromoCodeVM);
        }

        public async Task<IActionResult> AllOrders()
        {
            List<Order> completedOrders = await _repository.GetAllCompletedOrdersAsync();
            List<OrderVM> completedOrdersVM = new List<OrderVM>();
            foreach (var item in completedOrders)
            {
                completedOrdersVM.Add(new OrderVM(item));
            }
            return View("Orders", completedOrdersVM);
        }

        public async Task<IActionResult> DeliveredOrders()
        {
            List<Order> deliveredOrders = await _repository.GetDeliveredOrdersAsync();
            List<OrderVM> completedOrdersVM = new List<OrderVM>();
            foreach (var item in deliveredOrders)
            {
                completedOrdersVM.Add(new OrderVM(item));
            }
            return View("Orders", completedOrdersVM);
        }

        public async Task<IActionResult> PromoCodes()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View(promoCodesVM);
        }

        public async Task<IActionResult> RemoveProduct(Guid productID)
        {
            await _repository.DeleteProductAsync(productID);
            return RedirectToAction("Index", "WebShop");
        }
    }
}