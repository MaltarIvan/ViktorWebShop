using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ManageWebPage
{
    public class AddNewProductVM
    {
        [Required, MinLength(3)]
        public string Name { get; set; }
        [Required, MinLength(3)]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string ImageName { get; set; }
    }
}
