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
        public byte[] ImageData { get; set; }

        public Product(string name, string description, double price, byte[] imageData)
        {
            ProductID = Guid.NewGuid();
            Name = name;
            DateAdded = DateTime.Now;
            Description = description;
            Price = price;
            ImageData = imageData;
        }

        public Product()
        {
        }
    }
}
