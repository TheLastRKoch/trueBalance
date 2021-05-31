using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRueBalance.Data.Entities;
using TRueBalance.Models.MealViewModels;
using TRueBalance.Models.ProductViewModels;
using TRueBalance.Repositories.Meals;
using System;
using System.Collections.Generic;

namespace TRueBalance.Controllers
{
    [Authorize(Roles = "Developer,Administrator")]
    public class MealController : Controller
    {
        private readonly IMealRepository mealRepository;

        public MealController(IMealRepository _mealRepository)
        {
            mealRepository = _mealRepository;
        }

        public IActionResult Add()
        {
            MealViewModel model = new MealViewModel()
            {
                Name = "",
                Price = 0,
            };

            return View(model);
        }

        public IActionResult AddFromCSV()
        {
            return null;
        }

        public ViewResult List()
        {
            IEnumerable<Meal> products;
            products = mealRepository.GetList;
            return View(new MealListViewModel
            {
               Meals = products
            });
        }

        public IActionResult Update(int id)
        {
           Meal MealWithOldData = mealRepository.GetById(id);
           MealViewModel model = new MealViewModel()
            {
                MealID = MealWithOldData.MealID,
                Name = MealWithOldData.Name,
                Price = MealWithOldData.Price,
            };
            return View("Add", model);
        }

        public IActionResult Delete(int id)
        {
            mealRepository.Remove(id);
            return RedirectToAction("List");
        }

        public IActionResult DownloadMealsList()
        {
            mealRepository.DownloadToCsv("C:\\Users\\thela\\Desktop\\Meals2020.csv");
            return View();
        }

        [HttpPost]
        public IActionResult Add(MealViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (mealRepository.GetById(model.MealID) == null)
                {
                    mealRepository.Add(model);
                }
                else
                {
                   Meal MealWithOldData = mealRepository.GetById(model.MealID);
                   MealWithOldData.Name = model.Name;
                   MealWithOldData.Price = model.Price;
                    mealRepository.Update(MealWithOldData);
                }

                TempData["MessageType"] = "alert-success";
                TempData["Message"] = "Se ha guardado el platillo";

                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddFromCSV(MealViewModel model)
        {
            return null;
        }
    }
}
