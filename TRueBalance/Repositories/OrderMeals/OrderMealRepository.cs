using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRueBalance.Data.Entities;
using TRueBalance.Data;

namespace TRueBalance.Repositories.OrderMeals
{
    public class OrderMealRepository : IOrderMealRepository
    {
        private readonly ApplicationDbContext db;

        public OrderMealRepository(ApplicationDbContext appContex)
        {
            db = appContex;
        }

        /// <summary>
        /// Return a list of all OrderMeals
        /// </summary>
        public IEnumerable<OrderMeal> GetList
        {
            get
            {
                return db.OrderMeals.Include(x => x.Meal.Category).ToArray();
            }
        }
    }
}
