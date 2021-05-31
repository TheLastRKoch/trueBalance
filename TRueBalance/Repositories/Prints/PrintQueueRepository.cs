using Microsoft.AspNetCore.Http;
using TRueBalance.Data.Entities;
using TRueBalance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.Prints
{
    public class PrintQueueRepository : IPrintQueueRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpUserManage;

        public PrintQueueRepository(ApplicationDbContext _ApplicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.db = _ApplicationDbContext;
            this.httpUserManage = httpContextAccessor;
        }

        public void PrintWithCopy(Invoice CurrentInvoice)
        {
            Console.WriteLine("Void 😁");
        }

        public void Print(Invoice CurrentInvoice)
        {
            PrintQueue NewPrintElement = new PrintQueue()
            {
                LinkedInvoice = CurrentInvoice
            };
            db.PrintQueues.Add(NewPrintElement);
            db.SaveChanges();
        }
    }
}
