using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Data.Entities
{
    public class OrderMeal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderMealID { get; set; }
        public virtual Meal Meal { get; set; }
        public virtual Order Order { get; set; }
    }
}
