using TRueBalance.Data.Entities;
using TRueBalance.Models.SellViewModels;
using System;
using System.Collections.Generic;

namespace TRueBalance.Repositories.Orders
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetList { get; }
        int CountDayOrders();

        int CountActiveOrders();
        Order GetById(int OrderID);
        int GetLastID();
        void Add(SellViewModel model);
        void RemoveAddFromQueue(int OrderID);
        void Delete(int OrderID);
        void ClearDayOrders(DateTime CurrentDate);


    }
}
