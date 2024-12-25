using AutoMapper;
using Furniture_LebedevaS.DAL.Interface;
using Furniture_LebedevaS.Domain.Models;
using Furniture_LebedevaS.Domain.ViewModels.Products;
using Furniture_LebedevaS.Service;
using Microsoft.AspNetCore.Mvc;

namespace Furniture_LebedevaS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productService;
        private IMapper _mapper {  get; set; }
        MapperConfiguration _mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public ProductController(IProductsService productService)
        {
            _productService = productService;
            _mapper = _mapperConfiguration.CreateMapper();
            _productService = productService;
        }
        public IActionResult ListOfProducts(Guid Id, string categoryName)
        {
            var result = _productService.GetALLProductByCategoryId(Id);
            ListOfProductsViewModel listProduct=new ListOfProductsViewModel
            {
                Products=_mapper.Map<List<ProductForListOfProductsViewModel>> (result.Data),
                CategoryName =categoryName
                
            };
            return View(listProduct);
        }
    }
}
