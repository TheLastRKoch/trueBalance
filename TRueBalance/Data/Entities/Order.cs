using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Data.Entities
{
    public class Order
    {
        public Order ()
        {
            OrderMeals = new HashSet<OrderMeal>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public string ClientName { get; set; }
        public string State { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Observation { get; set; }

        public Category Takeaway { get; set; }

        public virtual ICollection<OrderMeal> OrderMeals { get; set; }
    }
}
