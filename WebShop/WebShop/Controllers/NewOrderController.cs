using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using WebShop.Core;
using WebShop.Utilities;
using WebShop.Models.NewOrder;

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
    }
}