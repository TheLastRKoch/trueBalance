using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRueBalance.Data;
using TRueBalance.Data.Entities;

namespace TRueBalance.Repositories.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext db;

        public CategoriesRepository(ApplicationDbContext appContex)
        {
            db = appContex;
        }

        public Category GetCategoryByID(int CategotyID)
        {
            return db.Categories.SingleOrDefault(x=>x.CategoryID == CategotyID);
        }

        public Category GetCategoryByName(string CategotyName)
        {
            return db.Categories.SingleOrDefault(x => x.Name == CategotyName);
        }

        public IEnumerable<Category> GetFoodCategories()
        {
            return db.Categories.Where(x => x.Type == Type.Food.ToString());
        }

        public IEnumerable<Category> GetSellCategories()
        {
            return db.Categories.Where(x => x.Type == Type.Sell.ToString());
        }
    }
}
