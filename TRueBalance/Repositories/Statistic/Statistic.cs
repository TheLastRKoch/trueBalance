using TRueBalance.Data.Entities;
using TRueBalance.Models.InvoiceViewModels;
using TRueBalance.Repositories.Meals;
using TRueBalance.Repositories.Orders;
using TRueBalance.Repositories.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRueBalance.Repositories.OrderMeals;

namespace TRueBalance.Repositories.Statistics
{
    public class Statistic : IStatistic
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderMealRepository orderMealsRepository;
        private readonly IMealRepository mealRepository;
        private string CurrentDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

        public Statistic(IInvoiceRepository _InvoiceRepository,
                          IOrderRepository _OrderRepository,
                          IOrderMealRepository _OrderMealsRepository,
                          IMealRepository _MealRepository)
        {
            this.invoiceRepository = _InvoiceRepository;
            this.orderRepository = _OrderRepository;
            this.orderMealsRepository = _OrderMealsRepository;
            this.mealRepository = _MealRepository;
        }

        /// <summary>
        /// Return the number of active orders
        /// </summary>
        /// <returns></returns>
        public int CountActiveOrders()
        {
            return orderRepository.GetList
                .Where(x => x.State == "Waiting")
                .Where(y=>y.StartTime.ToString().Contains(CurrentDate))
                .Count();
        }


        /// <summary>
        /// Get the number of the clients of the current day
        /// </summary>
        /// <returns></returns>
        public int GetClientNumber()
        {
            return invoiceRepository.GetList
                .Where(y => y.Date.ToString().Contains(CurrentDate))
                .Count();
        }

        /// <summary>
        /// Get the number of meals by category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public int GetNumberByCategory(int CategoryID)
        {
            int sum = 0;
            var test = orderMealsRepository.GetList;
            var query = from o in orderMealsRepository.GetList
                        where o.Meal.Category.CategoryID == CategoryID
                        group o by o.Meal.MealID into gp
                        select new
                        {
                            MealID = gp.Key,
                            Count = gp.Count(),
                        };
            int i = 0;

            while (i < query.Count())
            {
                sum += query.ElementAt(i).Count;
                i++;
            }
            return sum;
        }

        /// <summary>
        /// Return the AVG of the time that team take to fishied an order
        /// </summary>
        /// <returns></returns>
        public string GetOrderAVG()
        {
            try
            {
                TimeSpan Sum;
                double i = 0;
                var OrderList = orderRepository.GetList.Where(y => y.StartTime.ToString().Contains(CurrentDate));
                foreach (var order in OrderList)
                {
                    Sum += (order.EndTime - order.StartTime);
                    var test = Sum.TotalMinutes;
                    i++;
                }

                double Operation = Math.Round((Sum.TotalMinutes / i), 2, MidpointRounding.AwayFromZero);

                if (Operation < 0 || Sum == new TimeSpan())
                {
                    throw new System.InvalidOperationException("Invalid result");
                }

                return Operation + " mins";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "No disponible";
            }
        }

        /// <summary>
        /// Return the number of sells of each category 
        /// </summary>
        /// <param name="PaymentType"></param>
        /// <returns></returns>
        public int GetSellByType(int CategoryID)
        {
            return invoiceRepository.GetList
                .Where(x => x.PaymentType.CategoryID == CategoryID)
                .Where(y => y.Date.ToString().Contains(CurrentDate))
                .Count();
        }

        /// <summary>
        /// Get the name of the most seller product
        /// </summary>
        /// <returns></returns>
        public string GetTheMostSellMeal()
        {
            var OrderListv2 = orderMealsRepository.GetList;

            var query = from o in OrderListv2
                        group o by o.Meal.MealID into gp
                        select new
                        {
                            MealID = gp.Key,
                            Count = gp.Count(),
                        };

            if (query.Count() > 0)
            {
                int MostSellestMeal = query.OrderByDescending(x => x.Count).First().MealID;
                return mealRepository.GetById(MostSellestMeal).Name;
            }
            return "No disponible";
        }
    }
}
