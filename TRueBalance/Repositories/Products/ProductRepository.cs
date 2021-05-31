using Microsoft.EntityFrameworkCore;
using TRueBalance.Data.Entities;
using TRueBalance.Models.ProductViewModels;
using TRueBalance.Data;
using System.Collections.Generic;
using System.Linq;

namespace TRueBalance.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _appContext;

        public ProductRepository (ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public IEnumerable<Product> GetList
        {
            get
            {
                return _appContext.Products;
            }
        }

        void IProductRepository.Add(ProductViewModel model)
        {
            Product NewProduct = new Product()
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price,
                DueDate = model.DueDate,
            };
            _appContext.Products.Add(NewProduct);
            _appContext.SaveChanges();
        }

        void IProductRepository.Update(Product ProductWithNewData)
        {
            _appContext.Products.Attach(ProductWithNewData);
            _appContext.Entry(ProductWithNewData).State = EntityState.Modified;
            _appContext.SaveChanges();
        }

         void IProductRepository.Remove(int productId)
        {
            Product product = _appContext.Products.SingleOrDefault(x => x.ProductID == productId);
            _appContext.Products.Remove(product);
            _appContext.SaveChanges();
        }

        Product IProductRepository.GetById(int productId)
        {
            return _appContext.Products.FirstOrDefault(x => x.ProductID == productId);
        }
    }
}
