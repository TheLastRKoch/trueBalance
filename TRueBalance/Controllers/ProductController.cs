using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRueBalance.Data.Entities;
using TRueBalance.Models.ProductViewModels;
using TRueBalance.Repositories.Products;
using System;
using System.Collections.Generic;

namespace TRueBalance.Controllers
{
    [Authorize(Roles = "Developer")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Add()
        {

            ProductViewModel model = new ProductViewModel()
            {
                Code = "0000000000000",
                Name = "",
                Price = 0,
                DueDate = DateTime.MinValue
            };

            return View(model);
        }

        public ViewResult List()
        {
            IEnumerable<Product> products;
            products = _productRepository.GetList;
            return View(new ProductListViewModel
            {
                ProductList = products
            });
        }

        public IActionResult Update(int id)
        {
            Product ProductWithOldData = _productRepository.GetById(id);
            ProductViewModel model = new ProductViewModel()
            {
                Code = ProductWithOldData.Code,
                ProductID = ProductWithOldData.ProductID,
                Name = ProductWithOldData.Name,
                Price = ProductWithOldData.Price,
                DueDate = ProductWithOldData.DueDate
            };
            return View("Add", model);
        }

        public IActionResult Delete(int id)
        {
            _productRepository.Remove(id);
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_productRepository.GetById(model.ProductID) == null)
                {
                    _productRepository.Add(model);
                }
                else
                {
                    Product ProductWithOldData = _productRepository.GetById(model.ProductID);
                    ProductWithOldData.Name = model.Name;
                    ProductWithOldData.Price = model.Price;
                    ProductWithOldData.DueDate = model.DueDate;
                    _productRepository.Update(ProductWithOldData);
                }
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }
    }
}
