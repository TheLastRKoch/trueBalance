using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.Categories
{
    public enum Type
    {
        Food, Sell, PrintService
    }

    public interface ICategoriesRepository
    {
        Category GetCategoryByID(int CategotyID);
        Category GetCategoryByName(string CategotyName);
        IEnumerable<Category> GetFoodCategories();
        IEnumerable<Category> GetSellCategories();
    }
}
