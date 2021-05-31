using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRueBalance.Data.Entities
{
    public class Meal
    {

        public Meal ()
        {
            OrderMeals = new HashSet<OrderMeal>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MealID { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderMeal> OrderMeals { get; set; }
    }
}
