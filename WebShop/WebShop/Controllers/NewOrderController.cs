using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using WebShop.Core;
using WebShop.Utilities;
using WebShop.Models.NewOrder;
using MimeKit;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace WebShop.Controllers
{
    public class NewOrderController : Controller
    {
        private readonly IWebShopRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public NewOrderController(IWebShopRepository repository, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("Order/Index/{orderID}")]
        public async Task<IActionResult> Index(Guid orderID)
        {
            Core.Order order = await _repository.GetOrderAsync(orderID);
            NewOrderVM orderVM = new NewOrderVM(order);
            return View(orderVM);
        }

        public IActionResult AddNewOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrder(AddNewOrderVM addNewOrderVM)
        {
            if (ModelState.IsValid)
            {
                if (addNewOrderVM.MobilePhoneNumber == null && addNewOrderVM.PhoneNumber == null)
                {
                    ModelState.AddModelError("CustomError", "Please provide mobile phone number or telephone number.");
                    return View();
                }
                ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
                ShoppingCart shoppingCart = shoppingCartActions.GetShoppingCart();

                PromoCode promoCode = await _repository.GetPromoCodeAsync(addNewOrderVM.PromoCode);

                Order order = new Order
                {
                    ShoppingCart = shoppingCart,
                    ShoppingCartID = shoppingCart.ShoppingCartID,
                    OrderID = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Name = addNewOrderVM.Name,
                    Surname = addNewOrderVM.Surname,
                    StreetAdress1 = addNewOrderVM.StreetAdress1,
                    StreetAdress2 = addNewOrderVM.StreetAdress2,
                    City = addNewOrderVM.City,
                    PostalCode = addNewOrderVM.PostalCode,
                    Country = addNewOrderVM.Country,
                    Email = addNewOrderVM.Email,
                    MobilePhoneNumber = addNewOrderVM.MobilePhoneNumber,
                    PhoneNumber = addNewOrderVM.PhoneNumber,
                    PromoCode = promoCode,
                    IsCompleted = false,
                    IsDelivered = false,
                    PaymentMethod = addNewOrderVM.PaymentMethod
                };
                order.TotalPrice = order.GetTotalPrice();
                order.Discount = order.GetDiscount();
                await _repository.AddOrderAsync(order);
                return RedirectToAction("Index", new { orderID = order.OrderID});
            }
            return View();
        }

        public async Task<IActionResult> ChangeOrderDetails(Guid orderID)
        {
            Order order = await _repository.GetOrderAsync(orderID);
            string pC = "";
            if (order.PromoCode != null)
            {
                pC = order.PromoCode.Code;
            }
            ChangeOrderDetailsVM changeOrderDetailsVM = new ChangeOrderDetailsVM
            {
                OrderID = order.OrderID,
                Name = order.Name,
                Surname = order.Surname,
                StreetAdress1 = order.StreetAdress1,
                StreetAdress2 = order.StreetAdress2,
                City = order.City,
                PostalCode = order.PostalCode,
                Country = order.Country,
                Email = order.Email,
                MobilePhoneNumber = order.MobilePhoneNumber,
                PhoneNumber = order.PhoneNumber,
                PromoCode = pC,
                PaymentMethod = order.PaymentMethod
            };
            return View(changeOrderDetailsVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderDetails(ChangeOrderDetailsVM changeOrderDetailsVM)
        {
            if (ModelState.IsValid)
            {
                if (changeOrderDetailsVM.MobilePhoneNumber == null && changeOrderDetailsVM.PhoneNumber == null)
                {
                    ModelState.AddModelError("CustomError", "Please provide mobile phone number or telephone number.");
                    return View();
                }
                ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
                ShoppingCart shoppingCart = shoppingCartActions.GetShoppingCart();

                PromoCode promoCode = await _repository.GetPromoCodeAsync(changeOrderDetailsVM.PromoCode);

                Order order = new Order
                {
                    ShoppingCart = shoppingCart,
                    ShoppingCartID = shoppingCart.ShoppingCartID,
                    OrderID = changeOrderDetailsVM.OrderID,
                    DateCreated = DateTime.Now,
                    Name = changeOrderDetailsVM.Name,
                    Surname = changeOrderDetailsVM.Surname,
                    StreetAdress1 = changeOrderDetailsVM.StreetAdress1,
                    StreetAdress2 = changeOrderDetailsVM.StreetAdress2,
                    City = changeOrderDetailsVM.City,
                    PostalCode = changeOrderDetailsVM.PostalCode,
                    Country = changeOrderDetailsVM.Country,
                    Email = changeOrderDetailsVM.Email,
                    MobilePhoneNumber = changeOrderDetailsVM.MobilePhoneNumber,
                    PhoneNumber = changeOrderDetailsVM.PhoneNumber,
                    PromoCode = promoCode,
                    IsCompleted = false,
                    IsDelivered = false,
                    PaymentMethod = changeOrderDetailsVM.PaymentMethod
                };
                order.TotalPrice = order.GetTotalPrice();
                order.Discount = order.GetDiscount();
                await _repository.UpdateOrderAsync(order);
                return RedirectToAction("Index", new { orderID = order.OrderID });
            }
            return View();
        }

        public async Task<IActionResult> CancleOrder(Guid orderID)
        {
            Order order = await _repository.GetOrderAsync(orderID);
            await _repository.DeleteOrderAsync(order);
            return RedirectToAction("ShoppingCart", "WebShop");
        }

        public async Task<IActionResult> CompleteOrder(Guid orderID)
        {
            Order order = await _repository.GetOrderAsync(orderID);
            order.IsCompleted = true;
            if (order.PromoCode != null)
            {
                if (order.PromoCode.IsUsed)
                {
                    order.PromoCode = null;
                }
            }
            SendOrderEmailShop(order);
            SendOrderEmailClient(order);
            if (order.PromoCode != null)
            {
                order.PromoCode.IsUsed = true;
                await _repository.UpdatePromoCodeAsync(order.PromoCode);
            }
            ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
            ShoppingCart shoppingCart = new ShoppingCart(Guid.NewGuid());
            shoppingCart.CartItems = new List<CartItem>(shoppingCartActions.GetShoppingCart().CartItems);
            order.ShoppingCart = shoppingCart;
            order.ShoppingCartID = shoppingCart.ShoppingCartID;
            await _repository.UpdateOrderAsync(order);
            await shoppingCartActions.EmptyTheCartAsync();
            return RedirectToAction("Index", "WebShop");
        }

        private void SendOrderEmailShop(Order order)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress("narudzbe@istramushrooms.com"));
            message.From = new MailAddress("info@istramushrooms.com");
            message.Subject = "[Nova narudžba]";
            message.Body = "<h3>Napravljena je nova narudžba:</h3><br/><hr/> " + order.ToEmailString();

            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("mail.istramushrooms.com", 587);
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("info@istramushrooms.com", "MamaTata+-2");
            smtp.Send(message);
        }

        private void SendOrderEmailClient(Order order)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(order.Email));
            message.From = new MailAddress("info@istramushrooms.com");
            message.Subject = "[Nova narudžba]";
            message.Body = "<h3>Upravo ste naručili:</h3><br/><hr/>" + order.ToEmailString() + "<hr/><br/> Uskoro ćemo vam se javiti s detaljima o plačanju i preuzimanju vaše narudžbe!";

            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("mail.istramushrooms.com", 587);
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("info@istramushrooms.com", "MamaTata+-2");
            smtp.Send(message);
        }
    }
}