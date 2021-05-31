using Microsoft.EntityFrameworkCore;
using TRueBalance.Data.Entities;
using TRueBalance.Models.MealViewModels;
using TRueBalance.Data;
using System.Collections.Generic;
using System.Linq;
using TRueBalance.Repositories.CSVManager;

namespace TRueBalance.Repositories.Meals
{
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDbContext db;
        //private readonly CSVRepository csvManager;

        public MealRepository(ApplicationDbContext _appContext)
        {
            db = _appContext;
        }

        public IEnumerable<Meal> GetList
        {
            get
            {
                return db.Meals.Include(x => x.Category).OrderBy(x => x.Category);
            }
        }

        /// <summary>
        /// download all the meals to a CSV file
        /// </summary>
        /// <param name="path">Path where the CSV will be saved</param>
        /**
        public void DownloadToCsv(string path)
        {
            List<MealModel> catedList = new List<MealModel>();
            foreach (var Meal in db.Meals.Include(x => x.Category).ToList())
            {
                catedList.Add(new MealModel(Meal.Name, Meal.Price, Meal.Category.Name));
            }
            csvManager.MealsToCSV(path, catedList);
        }
        */

        public void DownloadToCsv(string path)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Load the all the meals from CSV Warning the load meals will replace all the existing one
        /// </summary>
        /// <param name="path">Path where is located the CSV file</param>
        public void LoadFromCsv(string path)
        {
            throw new System.NotImplementedException();
        }

        void IMealRepository.Add(MealViewModel model)
        {
            Meal NewMeal = new Meal()
            {
                MealID = model.MealID,
                Name = model.Name,
                Price = model.Price
            };
            db.Meals.Add(NewMeal);
            db.SaveChanges();
        }

        Meal IMealRepository.GetById(int mealId)
        {
            return db.Meals.SingleOrDefault(x => x.MealID == mealId);
        }

        void IMealRepository.Remove(int mealId)
        {
            var SelectedMeal = db.Meals.SingleOrDefault(x => x.MealID == mealId);
            db.Meals.Remove(SelectedMeal);
            db.SaveChanges();
        }

        void IMealRepository.Update(Meal MealWithNewData)
        {
            db.Meals.Attach(MealWithNewData);
            db.Entry(MealWithNewData).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
