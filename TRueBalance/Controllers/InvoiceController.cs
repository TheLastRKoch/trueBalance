using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TRueBalance.Data.Entities;
using TRueBalance.Models.InvoiceViewModels;
using TRueBalance.Repositories.Meals;
using TRueBalance.Repositories.Invoices;
using TRueBalance.Repositories.Prints;
using System.Collections.ObjectModel;

namespace TRueBalance.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {

        private readonly IInvoiceRepository InvoiceRepository;
        private readonly IMealRepository mealRepository;
        private readonly IPrintQueueRepository printQueueRepository;

        public InvoiceController(
            IInvoiceRepository _InvoiceRepository,
            IMealRepository _MealRepository,
            IPrintQueueRepository _PrintQueueRepository)
        {
            this.InvoiceRepository = _InvoiceRepository;
            this.mealRepository = _MealRepository;
            this.printQueueRepository = _PrintQueueRepository;
        }

        public ViewResult List()
        {
            InvoiceListViewModel model = new InvoiceListViewModel();
            model.Invoices = InvoiceRepository.GetList;
            return View(model);
        }

        public IActionResult Update(int InvoiceId)
        {
            Invoice InvoiceWithOldData = InvoiceRepository.GetById(InvoiceId);
            InvoiceViewModel model = new InvoiceViewModel()
            {
                InvoiceID = InvoiceWithOldData.InvoiceID,
                ClientName = InvoiceWithOldData.ClientName
            };
            return View("Add", model);
        }

        [Authorize(Roles = "Developer,Administrator")]
        public IActionResult ChangeState(int id)
        {
            InvoiceRepository.Activate(id);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Developer")]
        public IActionResult Delete(int id)
        {
            InvoiceRepository.Remove(id);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Print by Invoice IF
        /// </summary>
        /// <param name="id">InvoiceID if 0 then print last</param>
        /// <returns></returns>
        public IActionResult Print(int id)
        {
            if (id == 0)
                id = InvoiceRepository.GetLast().InvoiceID;

            InvoicePrintViewModel model = new InvoicePrintViewModel()
            {
                InvoiceID = id
            };
            return View("Print", model);
        }

        public JsonResult GetJson(int id)
        {
            return Json(InvoiceRepository.GiveFormatInvoice(id));
        }

        public IActionResult Show(int id)
        {
            Invoice CurrentInvoice = InvoiceRepository.GetById(id);
            InvoiceViewModel model = new InvoiceViewModel()
            {
                InvoiceID = CurrentInvoice.InvoiceID,
                ClientName = CurrentInvoice.ClientName,
                Date = CurrentInvoice.Date,
                TotalAmount = CurrentInvoice.TotalToPay,
                SellList = CurrentInvoice.LinkedSells
            };

            return View(model);
        }

        public IActionResult PrintCurrentInvoice(int id)
        {
            Invoice CurrentInvoice = InvoiceRepository.GetById(id);

            printQueueRepository.PrintWithCopy(CurrentInvoice);
            TempData["URL"] = "List/Invoice";
            return RedirectToAction("Print", "Sell");
        }

        [HttpPost]
        public IActionResult Add(InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (InvoiceRepository.GetById(model.InvoiceID) == null)
                {
                    Invoice NewInvoice = new Invoice()
                    {
                        InvoiceID = model.InvoiceID,
                        ClientName = model.ClientName
                    };
                }
                else
                {
                    Invoice InvoiceWithOldData = InvoiceRepository.GetById(model.InvoiceID);
                    InvoiceWithOldData.ClientName = model.ClientName;
                    InvoiceRepository.Update(InvoiceWithOldData);
                }
                return RedirectToAction("List");
            }
            return View();
        }
    }
}
