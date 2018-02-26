﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.ManageWebPage;
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
                Decimal priceDec = new Decimal();
                try
                {
                    priceDec = Convert.ToDecimal(priceStr);
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("CustomError", "The price format is incorrect.");
                    addNewProductVM.Price = null;
                    return View(addNewProductVM);
                }
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
                    BinaryReader reader = new BinaryReader(addNewProductVM.Image.OpenReadStream());
                    byte[] data = reader.ReadBytes((int)addNewProductVM.Image.Length);
                    Product product = new Product(addNewProductVM.Name, addNewProductVM.Description, Convert.ToDouble(priceDec), data);
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

        public IActionResult AddPictureToGallery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPictureToGallery(AddPictureVM addPictureVM)
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
                if (!validImageTypes.Contains(addPictureVM.Image.ContentType))
                {
                    ModelState.AddModelError("CustomError", "Please choose either a GIF, JPG or PNG image.");
                    addPictureVM.Image = null;
                    return View(addPictureVM);
                }
                else
                {
                    BinaryReader reader = new BinaryReader(addPictureVM.Image.OpenReadStream());
                    byte[] data = reader.ReadBytes((int)addPictureVM.Image.Length);
                    Picture picture = new Picture(addPictureVM.Description, data);
                    await _repository.AddPictureAsync(picture);
                    return RedirectToAction("Index");
                }
            }
            return View(addPictureVM);
        }

        public async Task<IActionResult> DeletePictureFromGallery(Guid pictureID)
        {
            await _repository.DeletePictureAsync(pictureID);
            return RedirectToAction("Gallery", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUnusedShoppingCartsAndOrders()
        {
            int count = await _repository.DeleteUnusedShoppingCartsAndOrdersAsync();
            return Json(count);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePromoCode(Guid promoCodeID)
        {
            PromoCode promoCode = await _repository.GetPromoCodeAsync(promoCodeID);
            await _repository.DeletePromoCodeAsync(promoCode);
            return Json(promoCodeID);
        }

        [HttpPost]
        public async Task<IActionResult> SetOrderAsDelivered(Guid orderID)
        {
            Order order = await _repository.GetOrderAsync(orderID);
            order.IsDelivered = true;
            await _repository.UpdateOrderAsync(order);
            return Json(orderID);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(Guid orderID)
        {
            Order order = await _repository.GetOrderAsync(orderID);
            await _repository.DeleteOrderAsync(order);
            return Json(orderID);
        }

        public async Task<IActionResult> ChangeProductDetails(Guid productID)
        {
            Product product = await _repository.GetProductAsync(productID);
            ChangeProductDetailsVM changeProductDetailsVM = new ChangeProductDetailsVM
            {
                Price = product.Price.ToString().Replace(",", "."),
                Description = product.Description,
                ProductID = product.ProductID
            };
            return View(changeProductDetailsVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProductDetails(ChangeProductDetailsVM changeProductDetailsVM)
        {
            string priceStr = changeProductDetailsVM.Price;
            priceStr = priceStr.Replace(".", ",");
            Decimal priceDec = Convert.ToDecimal(priceStr);
            bool isPriceFormat = Decimal.Round(priceDec, 2) == priceDec;
            if (ModelState.IsValid)
            {
                if (!isPriceFormat)
                {
                    ModelState.AddModelError("CustomError", "The price format is incorrect.");
                    changeProductDetailsVM.Price = null;
                    return View(changeProductDetailsVM);
                }
                Product product = await _repository.GetProductAsync(changeProductDetailsVM.ProductID);
                product.Price = Convert.ToDouble(priceDec);
                product.Description = changeProductDetailsVM.Description;
                await _repository.UpdateProductAsync(product);
                return RedirectToAction("Index", "WebShop");
            }
            return View(changeProductDetailsVM);
        }
    }
}