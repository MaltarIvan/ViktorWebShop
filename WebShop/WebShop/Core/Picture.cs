using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class Picture
    {
        [Key]
        public Guid PictureID { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public byte[] Data { get; set; }

        public Picture(string description, byte[] data)
        {
            PictureID = Guid.NewGuid();
            DateAdded = DateTime.Now;
            Description = description;
            Data = data;
        }

        public Picture()
        {

        }
    }
}
