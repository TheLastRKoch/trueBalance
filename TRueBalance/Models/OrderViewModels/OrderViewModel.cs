using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Models.OrderViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Meal> LinkedMeals { get; set; }
        public IEnumerable<Order> OrderList { get; set; }

        public int OrderID { get; set; }
        public string ClientName { get; set; }
        public string State { get; set; }
        public string Time { get; set; }
        public string Observation { get; set; }
        public string OrderType { get; set; }
    }
}
