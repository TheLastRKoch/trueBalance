using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRueBalance.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using TRueBalance.Data.Entities;
using TRueBalance.Models.InvoiceViewModels;

namespace TRueBalance.Repositories.Invoices
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext db;

        public InvoiceRepository(ApplicationDbContext appContex)
        {
            db = appContex;
        }

        public bool HasInvoices(DateTime CurrentDate)
        {
            if (db.Invoices.Where(x => x.Date.Day == CurrentDate.Day).FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Return a list of all invoices
        /// </summary>
        public IEnumerable<Invoice> GetList
        {
            get
            {
                return db.Invoices.Include(x => x.PaymentType).OrderByDescending(x=>x.InvoiceID);
            }
        }

        /// <summary>
        /// Add a new invoice
        /// </summary>
        /// <param name="model"></param>
        public void Add(InvoiceViewModel model)
        {
            Invoice NewInvoice = new Invoice()
            {
                InvoiceID = model.InvoiceID,
                ClientName = model.ClientName
            };
            db.Invoices.Add(NewInvoice);
            db.SaveChanges();
        }

        /// <summary>
        /// Return The last created Invoice
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public Invoice GetLast()
        {
            var Element = db.Invoices
                .Include(x => x.LinkedSells)
                .Include(x => x.PaymentType).OrderByDescending(x=>x.InvoiceID)
                .FirstOrDefault();

            foreach (var sell in Element.LinkedSells)
            {
                var CurrentSell = db.Sells.Include(s => s.Meal).SingleOrDefault(x => x.SellID == sell.SellID);
                sell.Meal = CurrentSell.Meal;
            }

            return Element;
        }

        /// <summary>
        /// Return an Invoice for the ID
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public Invoice GetById(int InvoiceID)
        {
            var Element = db.Invoices
                .Include(x => x.LinkedSells)
                .Include(x => x.PaymentType)
                .Where(x => x.InvoiceID == InvoiceID)
                .SingleOrDefault();

            foreach (var sell in Element.LinkedSells)
            {
                var CurrentSell = db.Sells.Include(s => s.Meal).SingleOrDefault(x => x.SellID == sell.SellID);
                sell.Meal = CurrentSell.Meal;
            }

            return Element;
        }

        /// <summary>
        /// Delete the invoce and any foreing key
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            Invoice SelectedInvoice = db.Invoices.
                Include(x => x.LinkedSells)
                .Where(x => x.InvoiceID == id)
                .SingleOrDefault();

            //Detete the Print element if has one
            PrintQueue SelectdPrintElement = db.PrintQueues.SingleOrDefault(x => x.PrintElementID == SelectedInvoice.InvoiceID);
            if (SelectdPrintElement != null)
            {
                db.PrintQueues.Remove(SelectdPrintElement);
            }

            //delete on cascade sells
            foreach (var sell in SelectedInvoice.LinkedSells)
            {
                db.Sells.Attach(sell);
                db.Sells.Remove(sell);
            }
            db.Invoices.Remove(SelectedInvoice);
            db.SaveChanges();
        }

        /// <summary>
        /// Change the state betwen "Activada" and "Desactivada"
        /// </summary>
        /// <param name="id"></param>
        public void Activate(int id)
        {
            Invoice SelectedInvoice = db.Invoices.SingleOrDefault(x => x.InvoiceID == id);
            if (SelectedInvoice.State.Equals(States.Activada.ToString()) != true)
            {
                SelectedInvoice.State = States.Activada.ToString();
            }
            else
            {
                SelectedInvoice.State = States.Desactivada.ToString();
            }
            db.Invoices.Attach(SelectedInvoice);
            db.Entry(SelectedInvoice).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Update the Invoice
        /// </summary>
        /// <param name="InvoiceWithNewData"></param>
        public void Update(Invoice InvoiceWithNewData)
        {
            db.Invoices.Attach(InvoiceWithNewData);
            db.Entry(InvoiceWithNewData).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Get a summation of the today invoices
        /// </summary>
        /// <returns></returns>
        public int GetDayEarnigs(DateTime CurrentDate)
        {
            return db.Invoices.Where(x => x.Date.Day == CurrentDate.Day).Sum(x => x.TotalToPay);
        }

        /// <summary>
        /// Get the first id of the first invoice of the current day
        /// </summary>
        /// <returns></returns>
        public int GetFistInvoiceID(DateTime CurrentDate)
        {
            return db.Invoices.FirstOrDefault().InvoiceID;
        }

        /// <summary>
        /// Get the first id of the last invoice of the current day
        /// </summary>
        /// <returns></returns>
        public int GetLastInvoiceID(DateTime CurrentDate)
        {
            return db.Invoices.OrderByDescending(x => x.InvoiceID).FirstOrDefault().InvoiceID;
        }

        /// <summary>
        /// Get the number of invoices the on a current date
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public int GetNumberOfClients(DateTime CurrentDate)
        {
            return db.Invoices.Where(x => x.Date.Day == CurrentDate.Day).Count();
        }

        /// <summary>
        /// Get the AVG of earnigs per client on a current date
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public int GetAVGPerClient(DateTime CurrentDate)
        {
            int sum = GetDayEarnigs(CurrentDate);
            int Number = GetNumberOfClients(CurrentDate);
            return sum / Number;
        }

        /// <summary>
        /// Get a summation of the today invoices made it with cash
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public int GetCashEarnigs(DateTime CurrentDate)
        {
            return db.Invoices.Where(x => x.Date.Day == CurrentDate.Day && x.PaymentType.Name == "Efectivo").Sum(x => x.TotalToPay);
        }

        /// <summary>
        /// Get a summation of the today invoices made it with credit cards
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public int GetCreditCardEarnigs(DateTime CurrentDate)
        {
            return db.Invoices.Where(x => x.Date.Day == CurrentDate.Day && x.PaymentType.Name == "Tarjeta").Sum(x => x.TotalToPay);
        }

        private List<InvoiceMeal> FormatMealList(int id)
        {
            List<InvoiceMeal> FinalMealList = new List<InvoiceMeal>();
            var MealList = GetList
                .Where(x => x.InvoiceID == id)
                .SelectMany(x => x.LinkedSells.Select(y => y.Meal));
            Meal CurrentMeal = null;
            for (int i = 0; i < MealList.Count(); i++)
            {
                CurrentMeal = (Meal)MealList.ElementAt(i);
                FinalMealList.Add(new InvoiceMeal(CurrentMeal.Name, CurrentMeal.Price));
            }
            return FinalMealList;
        }

        public InvoiceFormat GiveFormatInvoice(int id)
        {
            var CurrentInvoice = GetById(id);

            //Fill the invoice info
            InvoiceFormat InvoiceFormated = new InvoiceFormat()
            {
                InvoiceID = CurrentInvoice.InvoiceID,
                Date = CurrentInvoice.Date.ToString("dd/MM/yyyy"),
                Vendor = CurrentInvoice.Vendor,
                ClientName = CurrentInvoice.ClientName,
                Products = FormatMealList(id),
                PaymentType = CurrentInvoice.PaymentType.Name,
                TotalToPay = CurrentInvoice.TotalToPay,
                Cash = CurrentInvoice.Cash,
                ClientChange = CurrentInvoice.Cash - CurrentInvoice.TotalToPay
            };
            return InvoiceFormated;
        }
    }
}
