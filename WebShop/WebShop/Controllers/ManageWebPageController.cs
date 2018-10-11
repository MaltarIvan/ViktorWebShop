using System;
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
            /*
            var validImageTypes = new string[]
            {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
            };
            */
            if (ModelState.IsValid)
            {
                string priceStr = addNewProductVM.Price;
                priceStr = priceStr.Replace(".", ",");
                Decimal priceDec = Convert.ToDecimal(priceStr);
                bool isPriceFormat = Decimal.Round(priceDec, 2) == priceDec;
                if (addNewProductVM.ImageName.Contains(".gif") || addNewProductVM.ImageName.Contains(".jpeg") || addNewProductVM.ImageName.Contains(".pjpeg") || addNewProductVM.ImageName.Contains(".png"))
                {
                    ModelState.AddModelError("CustomError", "Please choose either a GIF, JPG or PNG image.");
                    addNewProductVM.ImageName = null;
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
                    Product product = new Product(addNewProductVM.Name, addNewProductVM.Description, Convert.ToDouble(priceDec), addNewProductVM.ImageName);
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
                return RedirectToAction("PromoCodesDate");
            }
            return View(addNewPromoCodeVM);
        }

        public IActionResult AddNewPromoCodeAutomated()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPromoCodeAutomated(AddPromoCodeAutomatedVM addPromoCodeAutomatedVM)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                for (int i = 0; i < addPromoCodeAutomatedVM.NumberOfCodes; i++)
                {
                    PromoCode promoCode = new PromoCode(new string(Enumerable.Repeat(chars, addPromoCodeAutomatedVM.CodeLength).Select(s => s[random.Next(s.Length)]).ToArray()), addPromoCodeAutomatedVM.Category);
                    await _repository.AddPromoCodeAsync(promoCode);
                }
                return RedirectToAction("PromoCodesDate");
            }
            return View(addPromoCodeAutomatedVM);
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
            return View("PromoCodes", promoCodesVM);
        }

        public async Task<IActionResult> PromoCodesCategory()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            promoCodes = promoCodes.OrderBy(p => p.Category).ToList();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View("PromoCodes", promoCodesVM);
        }

        public async Task<IActionResult> PromoCodesCategory1()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            promoCodes = promoCodes.Where(p => p.Category == 1).OrderBy(p => p.Category).ToList();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View("PromoCodes", promoCodesVM);
        }

        public async Task<IActionResult> PromoCodesCategory2()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            promoCodes = promoCodes.Where(p => p.Category == 2).OrderBy(p => p.Category).ToList();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View("PromoCodes", promoCodesVM);
        }

        public async Task<IActionResult> PromoCodesCategory3()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            promoCodes = promoCodes.Where(p => p.Category == 3).OrderBy(p => p.Category).ToList();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View("PromoCodes", promoCodesVM);
        }

        public async Task<IActionResult> PromoCodesCategory4()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            promoCodes = promoCodes.Where(p => p.Category == 4).OrderBy(p => p.Category).ToList();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View("PromoCodes", promoCodesVM);
        }

        public async Task<IActionResult> PromoCodesCategory5()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            promoCodes = promoCodes.Where(p => p.Category == 5).OrderBy(p => p.Category).ToList();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View("PromoCodes", promoCodesVM);
        }

        public async Task<IActionResult> PromoCodesCategory6()
        {
            List<PromoCode> promoCodes = await _repository.GetAllPromoCodesAsync();
            promoCodes = promoCodes.Where(p => p.Category == 6).OrderBy(p => p.Category).ToList();
            List<PromoCodeVM> promoCodesVM = new List<PromoCodeVM>();
            foreach (var item in promoCodes)
            {
                promoCodesVM.Add(new PromoCodeVM(item));
            }
            return View("PromoCodes", promoCodesVM);
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
            /*
            var validImageTypes = new string[]
            {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
            };
            */
            if (ModelState.IsValid)
            {
                if (addPictureVM.ImageName.Contains(".gif") || addPictureVM.ImageName.Contains(".jpeg") || addPictureVM.ImageName.Contains(".pjpeg") || addPictureVM.ImageName.Contains(".png"))
                {
                    ModelState.AddModelError("CustomError", "Please choose either a GIF, JPG or PNG image.");
                    addPictureVM.ImageName = null;
                    return View(addPictureVM);
                }
                else
                {
                    Picture picture = new Picture(addPictureVM.Description, addPictureVM.ImageName);
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