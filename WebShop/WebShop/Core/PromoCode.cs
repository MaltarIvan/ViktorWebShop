using Microsoft.AspNetCore.Mvc.Rendering;
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

        public static string FREE_1_TEXT = "1 Gratis";
        public static string FREE_2_TEXT = "2 Gratis";
        public static string DIS_100_HRK_TEXT = "Popust 100 kn";
        public static string DIS_200_HRK_TEXT = "Popust 200 kn";
        public static string DIS_50_PER_TEXT = "Popust 50%";
        public static string DIS_25_PER_TEXT = "Popust 25%";

        public static SelectListItem[] DISCOUNT_TYPE = new SelectListItem[] {
            new SelectListItem() { Text = FREE_1_TEXT, Value = FREE_1.ToString()},
            new SelectListItem() { Text = "2 Gratis", Value = FREE_2.ToString()},
            new SelectListItem() { Text = "Popust 100 kn", Value = DIS_100_HRK.ToString()},
            new SelectListItem() { Text = "Popust 200 kn", Value = DIS_200_HRK.ToString()},
            new SelectListItem() { Text = "Popust 50%", Value = DIS_50_PER.ToString()},
            new SelectListItem() { Text = "Popust 25%", Value = DIS_25_PER.ToString()}

        };

        [Key]
        public Guid PromoCodeID { get; set; }
        public string Code { get; set; }
        public int Category { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsUsed { get; set; }

        public PromoCode(string code, int category)
        {
            PromoCodeID = Guid.NewGuid();
            Code = code;
            Category = category;
            DateCreated = DateTime.Now;
            IsUsed = false;
        }

        public PromoCode()
        {

        }
    }
}
