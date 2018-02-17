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
        public string StreetAdress1 { get; set; }
        public string StreetAdress2 { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PromoCode { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDelivered { get; set; }

        public Order(ShoppingCart shoppingCart, string name, string surname, string streetAdress1, string streetAdress2, string city, int postalCode, string country, string email, string mobilePhoneNumber, string phoneNumber, string promoCode)
        {
            OrderID = Guid.NewGuid();
            DateCreated = DateTime.Now;
            ShoppingCartID = shoppingCart.ShoppingCartID;
            ShoppingCart = shoppingCart;
            Name = name;
            Surname = surname;
            StreetAdress1 = streetAdress1;
            StreetAdress2 = streetAdress2;
            City = city;
            Email = email;
            MobilePhoneNumber = mobilePhoneNumber;
            PhoneNumber = phoneNumber;
            PromoCode = promoCode;
            IsCompleted = false;
            IsDelivered = false;
        }

        public Order()
        {
        }

        public string ToEmailString()
        {
            double totalPrice = 0;
            string tableStyle = "border: 1px solid black;border-collapse: collapse;margin-bottom: 25px;width:100%;";
            string tableCellStyle = "border: 1px solid black;border-collapse: collapse;padding:5px;text-align:left;";

            string str = "<br/><table style=\"" + tableStyle + "\">" +
                "<thead><tr><th style=\"" + tableCellStyle + "\">Proizvod</th><th style=\"" + tableCellStyle + "\">Količina</th><th style=\"" + tableCellStyle + "\">Cijena po komadu</th><th style=\"" + tableCellStyle + "\">Ukupna cijena</th></tr></thead><tbody>";

            foreach (var item in ShoppingCart.CartItems)
            {
                totalPrice += item.PricePerItem * item.Quantity;
                str += "<tr><td style=\"" + tableCellStyle + "\">" + item.Product.Name +
                    "</td><td style=\"" + tableCellStyle + "\">" + item.Quantity +
                    "</td><td style=\"" + tableCellStyle + "\">" + item.PricePerItem + " kn" +
                    "</td><td style=\"" + tableCellStyle + "\">" + item.PricePerItem * item.Quantity + "kn " + 
                    "</td></tr>";
            }
            str += "</tbody></table><hr/><br/>" +
                "<br/><b>Ukupna cijena:</b> " + totalPrice + " kn<hr/>" +
                "<b>Broj naruđbe:</b> " + this.OrderID +
                "<br/><b>Datum:</b> " + this.DateCreated +
                "<br/><b>Ime:</b> " + Name +
                "<br/><b>Prezime:</b> " + Surname +
                "<br/><b>Adresa 1:</b> " + StreetAdress1 +
                "<br/><b>Adresa 2:</b> " + StreetAdress2 +
                "<br/><b>Grad:</b> " + City +
                "<br/><b>Poštanski broj:</b> " + PostalCode +
                "<br/><b>Država:</b> " + Country +
                "<br/><b>Email:</b> " + Email +
                "<br/><b>Broj mobitela:</b> " + MobilePhoneNumber +
                "<br/><b>Broj telefona:</b> " + PhoneNumber +
                "<br/><b>Promo Kod:</b> " + PromoCode;
            
            return str;
        }
    }
}
