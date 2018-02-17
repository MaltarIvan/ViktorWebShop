using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Core
{
    public class PromoCode
    {
        public static int FREE_1 = 1;
        public static int FREE_2 = 2;
        public static int DIS_100_HRK = 3;
        public static int DIS_200_HRK = 4;
        public static int DIS_50_PER = 5;
        public static int DIS_25_PER = 6;

        [Key]
        public Guid PromoCodeID { get; set; }
        public string Code { get; set; }
        public int Category { get; set; }

        public PromoCode(string code, int category)
        {
            PromoCodeID = Guid.NewGuid();
            Code = code;
            Category = category;
        }

        public PromoCode()
        {

        }
    }
}
