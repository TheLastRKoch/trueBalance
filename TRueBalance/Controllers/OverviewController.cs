using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRueBalance.Models;
using TRueBalance.Models.OverviewViewModels;
using TRueBalance.Repositories.Categories;
using TRueBalance.Repositories.Invoices;
using TRueBalance.Repositories.Orders;
using TRueBalance.Repositories.Statistics;
using TRueBalance.Repositories.UserSettings;

namespace TRueBalance.Controllers
{
    [Authorize]
    public class OverviewController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserSettingRepository userSettingRepository;
        private readonly ICategoriesRepository categoriesRepository;
        private readonly IStatistic stastics;

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

        public OverviewController(
            IInvoiceRepository _InvoiceRepository,
            IOrderRepository _OrderRepository,
            IUserSettingRepository _UserSettingRepository,
             ICategoriesRepository _CategoriesRepository,
            IStatistic _Stastics)
        {
            this.invoiceRepository = _InvoiceRepository;
            this.orderRepository = _OrderRepository;
            this.userSettingRepository = _UserSettingRepository;
            this.categoriesRepository = _CategoriesRepository;
            this.stastics = _Stastics;
        }

        /// <summary>
        ///  Make the CashBoxClosingFeature
        /// </summary>
        /// <returns></returns>
        public IActionResult ActiveteCashBoxClosingFeature()
        {
            if (invoiceRepository.HasInvoices(DateTime.Now.Date))
            {
                AddCookie("CashBoxClosingFeature", "Closed", 8, TimeExpresions.Hours);
                return RedirectToAction("Summary", "Overview");
            }
            TempData["MessageType"] = "alert-warning";
            TempData["Message"] = "No se puede realizar el cierre de caja sin tener ventas";
            return RedirectToAction("Dashboard", "Overview");
        }

        /// <summary>
        /// Setting for the Dashboard/index
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {

            if (TempData["MessageType"] != null)
            {
                ViewBag.MessageType = TempData["MessageType"].ToString();
                ViewBag.Message = TempData["Message"].ToString();
            }

            if (Glo.CashBoxClosingFeatureState == null)
            {
                Glo.CashBoxClosingFeatureState = userSettingRepository.Get("CashBoxClosingFeature").Value;
            }


            DashboardViewModel model = new DashboardViewModel();
            model.NumberOfOrders = orderRepository.CountDayOrders();
            if (model.NumberOfOrders > 3)
            {
                model.ActiveOrders = orderRepository.CountActiveOrders();
                model.BestSeller = stastics.GetTheMostSellMeal();
                model.OrderAVG = stastics.GetOrderAVG();
                model.ClientNumber = stastics.GetClientNumber();
            }
            return View(model);
        }

        /// <summary>
        /// Gets the caterories names and pass it to the view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<string> GetProductCategoryNames()
        {
            List<string> ListCategoryNames = new List<string>();
            foreach (var Category in categoriesRepository.GetFoodCategories())
            {
                ListCategoryNames.Add(Category.Name);
            }
            return ListCategoryNames;
        }

        /// <summary>
        /// Gets the count of the products by categoty and pass it to the view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<int> GetCountPerCategry()
        {
            List<int> CountPerProduct = new List<int>();
            foreach (var Category in categoriesRepository.GetFoodCategories())
            {
                CountPerProduct.Add(stastics.GetNumberByCategory(Category.CategoryID));
            }
            return CountPerProduct;
        }

        [HttpGet]
        public IList<string> GetPaymentTypeCategoryNames()
        {
            List<string> ListCategoryNames = new List<string>();
            foreach (var Category in categoriesRepository.GetSellCategories())
            {
                ListCategoryNames.Add(Category.Name);
            }
            return ListCategoryNames;
        }

        [HttpGet]
        public IList<int> GetCountPerPaymentType()
        {
            List<int> CountTypeOfSell = new List<int>();
            foreach (var Category in categoriesRepository.GetSellCategories())
            {
                CountTypeOfSell.Add(stastics.GetSellByType(Category.CategoryID));
            }
            return CountTypeOfSell;
        }

        public IActionResult Summary()
        {
            //Clear Orders
            if (GetCookie("CashBoxClosingFeature") != null)
            {
                //Get the date
                DateTime CurrentDate = DateTime.Now.Date;

                if (invoiceRepository.HasInvoices(DateTime.Now.Date))
                {

                    orderRepository.ClearDayOrders(CurrentDate);

                    SummaryViewModel model = new SummaryViewModel()
                    {
                        DayEarnigs = invoiceRepository.GetDayEarnigs(CurrentDate),
                        AVGClientEarnigs = invoiceRepository.GetAVGPerClient(CurrentDate),
                        DayCashEarnigs = invoiceRepository.GetCashEarnigs(CurrentDate),
                        DayCreditCardEarnigs = invoiceRepository.GetCreditCardEarnigs(CurrentDate),
                        NumberOfClients = invoiceRepository.GetNumberOfClients(CurrentDate),
                        FirstInvoiceID = invoiceRepository.GetFistInvoiceID(CurrentDate),
                        LastInvoiceID = invoiceRepository.GetLastInvoiceID(CurrentDate)
                    };
                    return View(model);
                }
                TempData["MessageType"] = "alert-warning";
                TempData["Message"] = "No se puede realizar el cierre de caja sin tener ventas";
                return RedirectToAction("Dashboard", "Overview");
            }
            TempData["MessageType"] = "alert-warning";
            TempData["Message"] = "No se pueden ver las estadisticas con la caja abierta";
            return RedirectToAction("Dashboard", "Overview");

        }
    }
}