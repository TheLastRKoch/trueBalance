using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Models.OverviewViewModels
{
    public class DashboardViewModel
    {
        public int NumberOfOrders { get; set; }

        public int ActiveOrders { get; set; }
        public string BestSeller { get; set; }
        public string OrderAVG { get; set; }
        public int ClientNumber { get; set; }
        public IList<int> CategoriesCountPerProduct { get; set; }

    }
}
