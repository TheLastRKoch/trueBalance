using Microsoft.AspNetCore.Http;
using TRueBalance.Data.Entities;
using TRueBalance.Models.SellViewModels;
using TRueBalance.Data;
using TRueBalance.Models;
using TRueBalance.Repositories.Dates;
using TRueBalance.Repositories.Prints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TRueBalance.Repositories.Categories;

namespace TRueBalance.Repositories.Sells
{

    public class SellRepository : ISellRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IPrintQueueRepository printRepository;
        private readonly IDateManageRepository dateManageRepository;
        private readonly ICategoriesRepository categoriesRepository;
        private readonly IHttpContextAccessor httpUserManage;

        public SellRepository(
            ApplicationDbContext _ApplicationDbContext,
            IDateManageRepository _DateManageRepository,
            IPrintQueueRepository _PrintRepository,
            ICategoriesRepository _CategoriesRepository,
            IHttpContextAccessor _HttpContextAccessor)
        {
            this.db = _ApplicationDbContext;
            this.dateManageRepository = _DateManageRepository;
            this.printRepository = _PrintRepository;
            this.categoriesRepository = _CategoriesRepository;
            this.httpUserManage = _HttpContextAccessor;
        }

        public IEnumerable<Sell> GetList
        {
            get
            {
                return db.Sells;
            }
        }

        void ISellRepository.Add(SellViewModel model)
        {
            var UserId = httpUserManage.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var User = db.Users.SingleOrDefault(x => x.Id == UserId);
            string UserName = string.Empty;
            int TotalAmount = 0;

            if (User == null)
            {
                UserName = "TRueBalance";
            }
            else
            {
                UserName = User.FirstName;
            }

            Invoice NewInvoice = new Invoice()
            {
                Vendor = UserName,
                ClientName = model.ClientName,
                Date = dateManageRepository.GetCurrentDate(),
                State = "Activada",
                TotalToPay = 0,
                Cash = model.Cash,
                PaymentType = categoriesRepository.GetCategoryByName(model.PaymentType)
            };

            for (int i = 0; i < model.MealsIdList.Count; i++)
            {
                int id = model.MealsIdList[i];
                var CurrentMeal = db.Meals.SingleOrDefault(x => x.MealID == id);
                Sell NewSell = new Sell()
                {
                    Meal = CurrentMeal
                };
                TotalAmount += NewSell.Meal.Price;
                db.Sells.Add(NewSell);
                NewInvoice.LinkedSells.Add(NewSell);
            }
            NewInvoice.TotalToPay = TotalAmount;
            db.Invoices.Add(NewInvoice);
            printRepository.PrintWithCopy(NewInvoice);
            db.SaveChanges();
        }

        Sell ISellRepository.GetById(int id)
        {
            return db.Sells.SingleOrDefault(x => x.SellID == id);
        }

        void ISellRepository.Update(Sell NewProduct)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meal> GetMealList
        {
            get
            {
                return db.Meals.OrderBy(x => x.Category);
            }
        }
    }
}
