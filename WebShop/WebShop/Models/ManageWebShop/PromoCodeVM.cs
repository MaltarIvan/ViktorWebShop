using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core;

namespace WebShop.Models.ManageWebShop
{
    public class PromoCodeVM
    {
        public Guid PromoCodeID { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsUsed { get; set; }

        public PromoCodeVM(PromoCode promoCode)
        {
            PromoCodeID = promoCode.PromoCodeID;
            Code = promoCode.Code;
            DateCreated = promoCode.DateCreated;
            IsUsed = promoCode.IsUsed;
            switch (promoCode.Category)
            {
                case 1:
                    Category = PromoCode.FREE_1_TEXT;
                    break;
                case 2:
                    Category = PromoCode.FREE_2_TEXT;
                    break;
                case 3:
                    Category = PromoCode.DIS_100_HRK_TEXT;
                    break;
                case 4:
                    Category = PromoCode.DIS_200_HRK_TEXT;
                    break;
                case 5:
                    Category = PromoCode.DIS_50_PER_TEXT;
                    break;
                case 6:
                    Category = PromoCode.DIS_25_PER_TEXT;
                    break;
            }
        }
    }
}
