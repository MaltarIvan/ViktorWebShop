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
                ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
                ShoppingCart shoppingCart = shoppingCartActions.GetShoppingCart();

                Core.Order order = new Core.Order
                {
                    ShoppingCart = shoppingCart,
                    ShoppingCartID = shoppingCart.ShoppingCartID,
                    OrderID = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Name = addNewOrderVM.Name,
                    Surname = addNewOrderVM.Surname,
                    StreetAdress = addNewOrderVM.StreetAdress,
                    City = addNewOrderVM.City,
                    PostalCode = addNewOrderVM.PostalCode,
                    Country = addNewOrderVM.Country,
                    Email = addNewOrderVM.Email,
                    PhoneNumber = addNewOrderVM.PhoneNumber,
                    Details = addNewOrderVM.Details,
                    PromoCode = addNewOrderVM.PromoCode,
                    Completed = false,
                    Delivered = false
                };
                await _repository.AddOrderAsync(order);
                return RedirectToAction("Index", new { orderID = order.OrderID});
            }
            return View();
        }

        public async Task<IActionResult> ChangeOrderDetails(Guid orderID)
        {
            Order order = await _repository.GetOrderAsync(orderID);
            ChangeOrderDetailsVM changeOrderDetailsVM = new ChangeOrderDetailsVM
            {
                OrderID = order.OrderID,
                Name = order.Name,
                Surname = order.Surname,
                StreetAdress = order.StreetAdress,
                City = order.City,
                PostalCode = order.PostalCode,
                Country = order.Country,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Details = order.Details,
                PromoCode = order.PromoCode
            };
            return View(changeOrderDetailsVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderDetails(ChangeOrderDetailsVM changeOrderDetailsVM)
        {
            if (ModelState.IsValid)
            {
                ShoppingCartActions shoppingCartActions = new ShoppingCartActions(HttpContext.Session, _repository);
                ShoppingCart shoppingCart = shoppingCartActions.GetShoppingCart();

                Order order = new Order
                {
                    ShoppingCart = shoppingCart,
                    ShoppingCartID = shoppingCart.ShoppingCartID,
                    OrderID = changeOrderDetailsVM.OrderID,
                    DateCreated = DateTime.Now,
                    Name = changeOrderDetailsVM.Name,
                    Surname = changeOrderDetailsVM.Surname,
                    StreetAdress = changeOrderDetailsVM.StreetAdress,
                    City = changeOrderDetailsVM.City,
                    PostalCode = changeOrderDetailsVM.PostalCode,
                    Country = changeOrderDetailsVM.Country,
                    Email = changeOrderDetailsVM.Email,
                    PhoneNumber = changeOrderDetailsVM.PhoneNumber,
                    Details = changeOrderDetailsVM.Details,
                    PromoCode = changeOrderDetailsVM.PromoCode,
                    Completed = false,
                    Delivered = false
                };
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
            SendOrderEmailShop(order);
            SendOrderEmailClient(order);
            return RedirectToAction("Index", "WebShop");
        }

        private void SendOrderEmailShop(Order order)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress("maltar.ivan@gmail.com"));
            message.From = new MailAddress("maltar.ivan@gmail.com");
            message.Body = "[Nova naruđba]";
            message.Body = "Napravljena je nova naruđba:<br/> " + order.ToEmailString();

            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("maltar.ivan@gmail.com", "Forerunner205a");
            smtp.Send(message);
        }

        private void SendOrderEmailClient(Order order)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(order.Email));
            message.From = new MailAddress("maltar.ivan@gmail.com");
            message.Body = "[Nova naruđba]";
            message.Body = "Upravo ste naručili:<br/>" + order.ToEmailString() + "<br/> Uskoro će mo vam se javiti s detaljima o plačanju i preuzimanju vaše naruđbe!";

            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("maltar.ivan@gmail.com", "Forerunner205a");
            smtp.Send(message);
        }
    }
}