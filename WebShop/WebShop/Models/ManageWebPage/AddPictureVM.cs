using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ManageWebPage
{
    public class AddPictureVM
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageName { get; set; }
    }
}
