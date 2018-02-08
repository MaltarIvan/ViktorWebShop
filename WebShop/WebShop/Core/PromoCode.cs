using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class PromoCode
    {
        [Key]
        public Guid PromoCodeID { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }

        public PromoCode(string code, double discount)
        {
            PromoCodeID = Guid.NewGuid();
            Code = code;
            Discount = discount;
        }
    }
}
