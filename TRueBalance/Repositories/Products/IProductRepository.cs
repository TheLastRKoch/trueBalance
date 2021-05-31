using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRueBalance.Data.Entities;
using TRueBalance.Models.ProductViewModels;

namespace TRueBalance.Repositories.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetList { get; }
        Product GetById(int id);
        void Add(ProductViewModel model);
        void Update(Product NewProduct);
        void Remove(int id);
    }
}
