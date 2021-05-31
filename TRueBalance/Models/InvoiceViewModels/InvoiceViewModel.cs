using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRueBalance.Models.InvoiceViewModels
{
    public class InvoiceViewModel
    {
        public ICollection<Sell> SellList { get; set; }

        public int InvoiceID { get; set; }

        [Required(ErrorMessage = "Este campo no puede quedar vacio")]
        public string ClientName { get; set; }

        public DateTime Date { get; set; }

        public int TotalAmount { get; set; }
    }
}
