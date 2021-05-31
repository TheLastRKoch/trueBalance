using TRueBalance.Models.InvoiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRueBalance.Data.Entities;

namespace TRueBalance.Repositories.Invoices
{
    public enum States { Activada, Desactivada }

    public struct InvoiceMeal
    {
        public InvoiceMeal(string _Name, int _Price)
        {
            this.Name = _Name;
            this.Price = _Price;
        }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public struct InvoiceFormat
    {
        public int InvoiceID { get; set; }

        public string Date { get; set; }
        public string Vendor { get; set; }

        public string ClientName { get; set; }

        public List<InvoiceMeal> Products { get; set; }
        public string PaymentType { get; set; }

        public int TotalToPay { get; set; }

        public int Cash { get; set; }

        public int ClientChange { get; set; }
    }

    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> GetList { get; }
        Invoice GetLast();
        Invoice GetById(int InvoiceID);
        bool HasInvoices(DateTime CurrentDate);
        void Add(InvoiceViewModel model);
        void Update(Invoice InvoiceWithNewData);
        void Remove(int InvoiceId);
        void Activate(int id);
        int GetFistInvoiceID(DateTime CurrentDate);
        int GetLastInvoiceID(DateTime CurrentDate);
        int GetNumberOfClients(DateTime CurrentDate);
        int GetAVGPerClient(DateTime CurrentDate);
        int GetDayEarnigs(DateTime CurrentDate);
        int GetCashEarnigs(DateTime CurrentDate);
        int GetCreditCardEarnigs(DateTime CurrentDate);
        InvoiceFormat GiveFormatInvoice(int id);
    }
}
