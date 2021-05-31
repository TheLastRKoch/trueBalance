using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRueBalance.Models.MealViewModels;

namespace TRueBalance.Repositories.CSVManager
{
    public interface ICSVRepository
    {
        List<MealModel> MealsFromCSV(string path);
        void MealsToCSV(string path, List<MealModel> objectList);
    }
}
