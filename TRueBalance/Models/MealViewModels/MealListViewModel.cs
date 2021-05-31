using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Models.MealViewModels
{
    public class MealListViewModel
    {
        public IEnumerable<Meal> Meals { get; set; }
    }
}
