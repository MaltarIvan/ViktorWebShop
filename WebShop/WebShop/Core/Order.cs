using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ShoppingCartID { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StreetAdress { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Details { get; set; }
        public string PromoCode { get; set; }

        public Order(ShoppingCart shoppingCart, string name, string surname, string streetAdress, string city, int postalCode, string country, string email, string phoneNumber, string details, string promoCode)
        {
            OrderID = Guid.NewGuid();
            DateCreated = DateTime.Now;
            ShoppingCartID = shoppingCart.ShoppingCartID;
            ShoppingCart = shoppingCart;
            Name = name;
            Surname = surname;
            StreetAdress = streetAdress;
            City = city;
            Email = email;
            PhoneNumber = phoneNumber;
            Details = details;
            PromoCode = promoCode;
        }

        public Order()
        {
        }
    }
}
