using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRueBalance.Models.SellViewModels
{
    public class SellViewModel
    {
        public IEnumerable<Meal> ListMeals { get; set; }

        public IList<int> MealsIdList { get; set; }

        public IList<string> MealsNameList { get; set; }

        public int SellId { get; set; }

        [Required(ErrorMessage = "Este campo no puede quedar vacio")]
        [Display(Name = "Digite el nombre del cliente")]
        public string ClientName { get; set; }

        [Display(Name = "Digite las observaciones")]
        public string Observations { get; set; }

        public int Cash { get; set; }

        public string PaymentType { get; set; }

        public string Vendor { get; set; }

        public string TakeawayType { get; set; }
    }
}
