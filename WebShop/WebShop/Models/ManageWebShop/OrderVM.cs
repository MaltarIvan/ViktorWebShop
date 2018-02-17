﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;
using WebShop.Models.Shared;

namespace WebShop.Models.ManageWebShop
{
    public class OrderVM
    {
        public ShoppingCartVM ShoppingCartVM;

        public Guid OrderID { get; set; }
        public Guid ShoppingCartID { get; set; }

        public DateTime DateCreated { get; set; }
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

        public OrderVM(Order order)
        {
            ShoppingCartVM = new ShoppingCartVM(order.ShoppingCart);
            ShoppingCartID = order.ShoppingCartID;
            OrderID = order.OrderID;

            DateCreated = order.DateCreated;
            Name = order.Name;
            Surname = order.Surname;
            StreetAdress = order.StreetAdress;
            City = order.City;
            PostalCode = order.PostalCode;
            Country = order.Country;
            Email = order.Email;
            PhoneNumber = order.PhoneNumber;
            Details = order.Details;
            PromoCode = order.PromoCode;
            Completed = order.Completed;
            Delivered = order.Delivered;
        }
    }
}