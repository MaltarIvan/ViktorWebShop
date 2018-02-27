using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ManageWebPage
{
    public class AddPromoCodeAutomatedVM
    {
        [Required]
        public int CodeLength { get; set; }

        [Required]
        public int NumberOfCodes { get; set; }

        [Required]
        public int Category { get; set; }
    }
}
