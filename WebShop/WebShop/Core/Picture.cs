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
        public string ImageName { get; set; }

        public Picture(string description, string imageName)
        {
            PictureID = Guid.NewGuid();
            DateAdded = DateTime.Now;
            Description = description;
            ImageName = imageName;
        }

        public Picture()
        {

        }
    }
}
