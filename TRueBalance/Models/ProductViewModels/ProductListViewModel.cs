using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Models.ProductViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }
    }
}
