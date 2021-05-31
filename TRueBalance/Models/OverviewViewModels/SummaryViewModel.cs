using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Models.OverviewViewModels
{
    public class SummaryViewModel
    {
        public int DayEarnigs { get; set; }
        public int DayCashEarnigs { get; set; }
        public int DayCreditCardEarnigs { get; set; }
        public int AVGClientEarnigs { get; set; }
        public int NumberOfClients { get; set; }
        public int FirstInvoiceID { get; set; }
        public int LastInvoiceID { get; set; }
    }
}
