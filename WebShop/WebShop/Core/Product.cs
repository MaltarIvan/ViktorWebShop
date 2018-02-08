using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }
        public DateTime DateAdded { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageName { get; set; }
        public int Quantity { get; set; }

        public Product(string name, string description, double price, string imageName, int quantity)
        {
            ProductID = Guid.NewGuid();
            Name = name;
            DateAdded = DateTime.Now;
            Description = description;
            Price = price;
            ImageName = imageName;
            Quantity = quantity;
        }

        public Product()
        {
        }
    }
}
