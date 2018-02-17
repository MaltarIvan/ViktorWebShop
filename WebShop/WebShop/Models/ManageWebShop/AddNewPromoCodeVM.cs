using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ManageWebShop
{
    public class AddNewPromoCodeVM
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public int Category { get; set; }
    }
}
