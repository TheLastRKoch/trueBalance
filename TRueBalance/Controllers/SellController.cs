using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TRueBalance.Models;
using TRueBalance.Models.SellViewModels;
using TRueBalance.Repositories.Meals;
using TRueBalance.Repositories.Orders;
using TRueBalance.Repositories.Sells;
using TRueBalance.Repositories.Invoices;
using TRueBalance.Repositories.UserSettings;
using System;
using System.Collections.Generic;

namespace TRueBalance.Controllers
{
    [Authorize]
    public class SellController : Controller
    {
        private readonly ISellRepository sellRepository;
        private readonly IMealRepository mealRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserSettingRepository userSettingRepository;

        #region CookiesManager
        private enum TimeExpresions
        {
            Minutes, Hours, Days, Years
        }

        private void AddCookie(string Key, string Value, int ExpireTime, TimeExpresions Time)
        {
            CookieOptions option = new CookieOptions();
            switch (Time)
            {
                case TimeExpresions.Minutes:
                    option.Expires = DateTime.Now.AddMinutes(ExpireTime);
                    break;

                case TimeExpresions.Hours:
                    option.Expires = DateTime.Now.AddHours(ExpireTime);
                    break;

                case TimeExpresions.Days:
                    option.Expires = DateTime.Now.AddDays(ExpireTime);
                    break;

                case TimeExpresions.Years:
                    option.Expires = DateTime.Now.AddYears(ExpireTime);
                    break;

                default:
                    option.Expires = DateTime.Now.AddMilliseconds(ExpireTime);
                    break;
            }
            Response.Cookies.Append(Key, Value, option);
        }

        private string GetCookie(string Key)
        {
            return Request.Cookies[Key];
        }

        private void DeleteCookie(string Key)
        {
            Response.Cookies.Delete(Key);
        }
        #endregion

        public SellController(
            ISellRepository _sellRepository,
            IMealRepository _mealRepository,
            IInvoiceRepository _invoiceRepository,
            IOrderRepository _orderRepository,
            IUserSettingRepository _userSettingRepository
            )
        {
            sellRepository = _sellRepository;
            mealRepository = _mealRepository;
            invoiceRepository = _invoiceRepository;
            orderRepository = _orderRepository;
            userSettingRepository = _userSettingRepository;
        }

        public IActionResult MakeSale()
        {
            if (GetCookie("CashBoxClosingFeature") == null)
            {
                if (TempData["MessageType"] != null)
                {
                    ViewBag.MessageType = TempData["MessageType"].ToString();
                    ViewBag.Message = TempData["Message"].ToString();
                }

                var MealsList = sellRepository.GetMealList;
                SellViewModel model = new SellViewModel()
                {
                    ListMeals = MealsList
                };
                return View(model);
            }
            else
            {
                TempData["MessageType"] = "alert-warning";
                TempData["Message"] = "Para poder realizar una venta es necesario reabrir la caja";
                return RedirectToAction("Index", "Dashboard");
            }
        }

        public void GetInfo(IList<int> SelectedMeals)
        {
            Glo.Meals = SelectedMeals;
        }

        [HttpGet]
        public IActionResult TakeArray(string[] Result)
        {
            List<int> Products = new List<int>();

            for (int i = 0; i < Result.Length; i++)
            {
                ///Products.Add(int.Parse(Result[i]));
            }
            Glo.Meals = Products;
            return View();
        }

        public IActionResult Print()
        {
            return View();
        }

        [HttpPost]
        public void MakeSale([FromBody] SellViewModel SellProductObject)
        {
            //Add the sell
            sellRepository.Add(SellProductObject);

            //Add the order
            orderRepository.Add(SellProductObject);
        }
    }
}