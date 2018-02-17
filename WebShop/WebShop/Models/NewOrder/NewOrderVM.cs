using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;
using WebShop.Models.Shared;

namespace WebShop.Models.NewOrder
{
    public class NewOrderVM
    {
        public ShoppingCartVM ShoppingCartVM;

        public Guid OrderID { get; set; }
        public Guid ShoppingCartID { get; set; }

        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StreetAdress1 { get; set; }
        public string StreetAdress2 { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PromoCode { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }

        public NewOrderVM(Order order)
        {
            ShoppingCartVM = new ShoppingCartVM(order.ShoppingCart);
            ShoppingCartID = order.ShoppingCartID;
            OrderID = order.OrderID;

            DateCreated = order.DateCreated;
            Name = order.Name;
            Surname = order.Surname;
            StreetAdress1 = order.StreetAdress1;
            StreetAdress2 = order.StreetAdress2;
            City = order.City;
            PostalCode = order.PostalCode;
            Country = order.Country;
            Email = order.Email;
            MobilePhoneNumber = order.MobilePhoneNumber;
            PhoneNumber = order.PhoneNumber;
            if (order.PromoCode != null)
            {
                if (order.PromoCode.IsUsed)
                {
                    PromoCode = "Ne Važeći Promo Kod";
                }
                else
                {
                    PromoCode = order.PromoCode.Code;
                }
            }
            TotalPrice = order.GetTotalPrice();
            Discount = order.GetDiscount();
        }
    }
}
