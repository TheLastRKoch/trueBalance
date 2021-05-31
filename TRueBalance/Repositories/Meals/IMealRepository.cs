using TRueBalance.Data.Entities;
using TRueBalance.Models.MealViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.Meals
{
    public interface IMealRepository
    {
        IEnumerable<Meal> GetList { get; }
        Meal GetById(int mealId);
        void Add(MealViewModel model);
        void Update(Meal MealWithNewData);
        void Remove(int mealId);

        void LoadFromCsv(string path);
        void DownloadToCsv(string path);
    }
}
