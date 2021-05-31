using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.Statistics
{
    public interface IStatistic
    {
        int CountActiveOrders();
        string GetTheMostSellMeal();
        string GetOrderAVG();
        int GetClientNumber();
        int GetNumberByCategory(int CategoryID);
        int GetSellByType(int CategoryID);
    }
}
