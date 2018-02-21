using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class Order
    {
        public static SelectListItem[] COUNTRIES = new SelectListItem[] {
            new SelectListItem() { Text = "Hrvatska", Value = "Hrvatska"}

        };

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
        public PromoCode PromoCode { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDelivered { get; set; }

        public Order(ShoppingCart shoppingCart, string name, string surname, string streetAdress1, string streetAdress2, string city, int postalCode, string country, string email, string mobilePhoneNumber, string phoneNumber, PromoCode promoCode)
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
            double totalPrice = GetTotalPrice();
            double discount = GetDiscount();
            string tableStyle = "border: 1px solid black;border-collapse: collapse;margin-bottom: 25px;width:100%;";
            string tableCellStyle = "border: 1px solid black;border-collapse: collapse;padding:5px;text-align:left;";

            string str = "<br/><table style=\"" + tableStyle + "\">" +
                "<thead><tr><th style=\"" + tableCellStyle + "\">Proizvod</th><th style=\"" + tableCellStyle + "\">Količina</th><th style=\"" + tableCellStyle + "\">Cijena po komadu</th><th style=\"" + tableCellStyle + "\">Ukupna cijena</th></tr></thead><tbody>";

            foreach (var item in ShoppingCart.CartItems)
            {
                str += "<tr><td style=\"" + tableCellStyle + "\">" + item.Product.Name +
                    "</td><td style=\"" + tableCellStyle + "\">" + item.Quantity +
                    "</td><td style=\"" + tableCellStyle + "\">" + item.PricePerItem + " kn" +
                    "</td><td style=\"" + tableCellStyle + "\">" + item.PricePerItem * item.Quantity + "kn " + 
                    "</td></tr>";
            }
            str += "</tbody></table><hr/><br/>" +
                "<br/><b>Ukupna cijena:</b> " + totalPrice + " kn" +
                "<br/><b>Popust:</b> " + discount + " kn" +
                "<br/><b>Za platiti:</b> " + (totalPrice - discount) + " kn<hr/>" +
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
                "<br/><b>Promo Kod:</b> ";
            if (PromoCode != null)
            {
                str += PromoCode.Code;
            }
            
            return str;
        }

        public double GetTotalPrice()
        {
            double totalPrice = 0;
            foreach (var item in this.ShoppingCart.CartItems)
            {
                totalPrice += item.PricePerItem * item.Quantity;
            }
            return totalPrice;
        }

        public double GetDiscount()
        {
            double totalPrice = GetTotalPrice();
            double discount = 0;

            if (this.PromoCode != null)
            {
                if (!this.PromoCode.IsUsed)
                {
                    List<CartItem> cartItems = this.ShoppingCart.CartItems.OrderByDescending(c => c.PricePerItem).ToList();
                    switch (this.PromoCode.Category)
                    {
                        case 1:
                            discount = cartItems.First().PricePerItem;
                            break;
                        case 2:
                            discount = cartItems.First().PricePerItem;
                            if (cartItems.First().Quantity >= 2)
                            {
                                discount *= 2;
                            }
                            else
                            {
                                if (cartItems.Count >= 2)
                                {
                                    discount += cartItems.Skip(1).First().PricePerItem;
                                }
                            }
                            break;
                        case 3:
                            if (totalPrice - 100 < 0)
                            {
                                discount = totalPrice;
                            }
                            else
                            {
                                discount = 100;
                            }
                            break;
                        case 4:
                            if (totalPrice - 200 < 0)
                            {
                                discount = totalPrice;
                            }
                            else
                            {
                                discount = 200;
                            }
                            break;
                        case 5:
                            discount = totalPrice * 0.5;
                            break;
                        case 6:
                            discount = totalPrice * 0.25;
                            break;

                        default:
                            break;
                    }
                }
            }

            return discount;
        }
    }
}
