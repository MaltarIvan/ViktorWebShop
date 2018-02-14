﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.NewOrder
{
    public class AddNewOrderVM
    {
        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, MinLength(3)]
        public string Surname { get; set; }

        [Required, MinLength(5)]
        public string StreetAdress { get; set; }

        [Required, MinLength(5)]
        public string City { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required, MinLength(5)]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone Number Required")]
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }
        
        public string Details { get; set; }
        
        public string PromoCode { get; set; }
    }
}
