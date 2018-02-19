using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;

namespace WebShop.Models.Home
{
    public class PictureVM
    {
        public Guid PictureID { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public PictureVM(Picture picture)
        {
            PictureID = picture.PictureID;
            Description = picture.Description;
            ImageName = picture.ImageName;
        }
    }
}
