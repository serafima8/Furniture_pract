using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Domain.ViewModels.Products
{
    public class ListOfProductsViewModel
    {
        public List<ProductForListOfProductsViewModel> Products { get; set; }
        public Guid CategorieId { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProductForListOfProductsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string PathImg { get; set; }
        public int Star { get; set; }
        public Guid CategorieId { get; set; }
    }
}