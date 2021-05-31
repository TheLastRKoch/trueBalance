using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Models.InvoiceViewModels
{
    public class InvoiceListViewModel
    {
        public IEnumerable<Invoice> Invoices { set; get; }
    }
}
