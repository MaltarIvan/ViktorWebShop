﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.ManageWebPage
{
    public class ChangeProductDetailsVM
    {
        [Required, MinLength(3)]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public Guid ProductID { get; set; }
    }
}
