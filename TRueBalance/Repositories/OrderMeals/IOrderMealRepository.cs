using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.OrderMeals
{
    public interface IOrderMealRepository
    {
        IEnumerable<OrderMeal> GetList { get; }
    }
}
