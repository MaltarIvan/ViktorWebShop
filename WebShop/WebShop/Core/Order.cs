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
        public bool Completed { get; set; }
        public bool Delivered { get; set; }

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
            Completed = false;
            Delivered = false;
        }

        public Order()
        {
        }

        public string ToEmailString()
        {
            double totalPrice = 0;

            string str = "Broj naruđbe: " + this.OrderID + "<br/>" +
                "Datum: " + this.DateCreated +
                "<br/><table><thead><tr><th>Proizvod</th><th>Količina</th><th>Cijena po komadu</th><th>Ukupnna cijena</th></tr></thead><tbody>";

            foreach (var item in ShoppingCart.CartItems)
            {
                totalPrice += item.PricePerItem * item.Quantity;
                str += "<tr><td>" + item.Product.Name + 
                    "</td><td>" + item.Quantity + 
                    "</td><td>" + item.PricePerItem + 
                    "</td><td>" + item.PricePerItem * item.Quantity + 
                    "</td></tr>";
            }
            str += "</tbody></table><br/>Ukupna cijena: " + totalPrice + 
                "<br/>Ime: " + Name + 
                "<br/>Prezime: " + Surname + 
                "<br/>Adresa: " + StreetAdress + 
                "<br/>Grad: " + City + 
                "<br/>Poštanski broj: " + PostalCode + 
                "<br/>Država: " + Country + 
                "<br/>Email: " + Email + 
                "<br/>Broj telefona: " + PhoneNumber + 
                "<br/>Detalji: " + Details + 
                "<br/>Promo Kod: " + PromoCode;

            return str;
        }
    }
}
