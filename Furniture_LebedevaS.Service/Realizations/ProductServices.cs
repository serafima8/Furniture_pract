using AutoMapper;
using Furniture_LebedevaS.DAL.Interface;
using Furniture_LebedevaS.Domain.Enum;
using Furniture_LebedevaS.Domain.Models;
using Furniture_LebedevaS.Domain.ModelsDb;
using Furniture_LebedevaS.Domain.Response;
using Furniture_LebedevaS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Service
{
    public class ProductService : IProductsService
    {
        private readonly IBaseStorage<ProductDb> _productStorage;
        private IMapper _mapper { get; set; }
        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });
        public ProductService(IBaseStorage<ProductDb> productStorage)
        {
            _productStorage = productStorage;
            _mapper = mapperConfiguration.CreateMapper();
        }

        public BaseResponse<List<Product>> GetALLProductByCategoryId(Guid Id)
        {
            try
            {
                var productDb = _productStorage.GetAll().Where(x => x.CategorieId == Id).OrderBy(p => p.Price).ToList();
                var result = _mapper.Map<List<Product>>(productDb);
                if (result.Count == 0)
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Product>>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<List<Product>> GetAllProducts()
        {

            try
            {
                var productDb = _productStorage.GetAll().OrderBy(p => p.Price).ToList();
                var result = _mapper.Map<List<Product>>(productDb);
                if (result.Count == 0)
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Product>>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
