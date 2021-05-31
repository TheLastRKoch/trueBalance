using TRueBalance.Data.Entities;
using TRueBalance.Models.SellViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.Sells
{
    public interface ISellRepository
    {
        IEnumerable<Sell> GetList { get; }
        Sell GetById(int id);

        void Add(SellViewModel model);
        void Update(Sell NewProduct);
        IEnumerable<Meal> GetMealList { get; }
    }
}
