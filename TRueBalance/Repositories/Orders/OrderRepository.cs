using Microsoft.EntityFrameworkCore;
using TRueBalance.Data.Entities;
using TRueBalance.Models.SellViewModels;
using TRueBalance.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TRueBalance.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext db;

        private enum State
        {
            [Description("En Espera")]
            Waiting,

            [Description("Listo")]
            Ready
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_db"></param>
        public OrderRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        /// <summary>
        /// Return the complete list of orders
        /// </summary>
        public IEnumerable<Order> GetList
        {
            get
            {
                var OrderList = db.Orders
                    .Include(x=>x.Takeaway)
                    .Include(x => x.OrderMeals);

                foreach (var order in OrderList)
                {
                    order.OrderMeals = db.OrderMeals.Where(x => x.Order.OrderID == order.OrderID).Include(x => x.Meal).ToArray();
                }

                return OrderList;
            }
        }

        public int GetLastID()
        {
            return db.Orders.Select(x => x.OrderID).Max();
        }

        public int CountDayOrders()
        {
            return db.Orders.Where(x => x.StartTime.ToString().Contains(DateTime.Now.Date.ToString("dd/MM/yyyy"))).Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        private Int16 AnalizeTakeAwayResult(string Result)
        {
            if (Result.Equals("Llevar"))
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="model"></param>
        public void Add(SellViewModel model)
        {
            DateTime MyTime = DateTime.Now;

            Order NewOrder = new Order()
            {
                ClientName = model.ClientName,
                Observation = model.Observations,
                State = State.Waiting.ToString(),
                StartTime = MyTime,
                Takeaway = db.Categories.SingleOrDefault(x => x.Name == model.TakeawayType)
            };
            foreach (var Meal in model.MealsIdList)
            {
                var CurrentMeal = db.Meals.SingleOrDefault(x => x.MealID == Meal);
                OrderMeal orderMeal = new OrderMeal()
                {
                    Order = NewOrder,
                    Meal = CurrentMeal
                };
                NewOrder.OrderMeals.Add(orderMeal);
                CurrentMeal.OrderMeals.Add(orderMeal);
                db.OrderMeals.Add(orderMeal);
            }
            db.Orders.Add(NewOrder);
            db.SaveChanges();
        }

        /// <summary>
        /// Return the order for each ID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public Order GetById(int OrderID)
        {
            var Element = db.Orders
                   .Include(x => x.OrderMeals)
                   .Where(x => x.OrderID == OrderID)
                   .SingleOrDefault();
            return Element;
        }

        /// <summary>
        /// Administrate the order from the static queue
        /// </summary>
        /// <param name="OrderID"></param>
        public void RemoveAddFromQueue(int OrderID)
        {
            Order SelectOrder = db.Orders.SingleOrDefault(x => x.OrderID == OrderID);
            if (SelectOrder.State.Equals(State.Waiting.ToString()))
            {
                SelectOrder.State = State.Ready.ToString();
                SelectOrder.EndTime = DateTime.Now;
            }
            else
            {
                SelectOrder.State = State.Waiting.ToString();
            }
            db.Entry(SelectOrder).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Delete the order from the DB
        /// </summary>
        /// <param name="OrderID"></param>
        public void Delete(int OrderID)
        {
            var SelectOrder = db.Orders.SingleOrDefault(x => x.OrderID == OrderID);
            var OrderMealList = db.OrderMeals.Where(x => x.Order.OrderID == SelectOrder.OrderID);
            //delete on cascate OrderMeals
            foreach (var OrderMeal in OrderMealList)
            {
                db.Attach(OrderMeal);
                db.OrderMeals.Remove(OrderMeal);
            }
            db.Attach(SelectOrder);
            db.Orders.Remove(SelectOrder);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete all the orders of the current date
        /// </summary>
        public void ClearDayOrders(DateTime CurretDate)
        {
            foreach (var order in db.Orders.Where(x => x.StartTime.Day == CurretDate.Day).ToList())
            {
                Delete(order.OrderID);
            }
        }

        public int CountActiveOrders()
        {
            return db.Orders
                .Where(x => x.StartTime.ToString().Contains(DateTime.Now.Date.ToString("dd/MM/yyyy")))
                .Where(x=>x.State == "Waiting")
                .Count();
        }
    }
}
