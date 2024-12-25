using AutoMapper;
using Furniture_LebedevaS.DAL.Interface;
using Furniture_LebedevaS.Domain.Models;
using Furniture_LebedevaS.Domain.ModelsDb;
using Furniture_LebedevaS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Service.Realizations
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseStorage<CategoryDb> _categoryStorage;
        private IMapper _mapper { get; set; }
        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public CategoryService(IBaseStorage<CategoryDb> categoryStorage)
        {
            _categoryStorage = categoryStorage;
            _mapper = mapperConfiguration.CreateMapper();
        }


        public BaseResponse<List<Category>> GetAllCategories()
        {
            var categoriesDb = _categoryStorage.GetAll().ToList();
            if (categoriesDb == null)
            {
                return new BaseResponse<List<Category>>()
                {
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Description = "Нет соединения с базой данных"
                };
            }
            var categories = _mapper.Map<List<Category>>(categoriesDb);

            if (categories.Count == 0)
            {
                return new BaseResponse<List<Category>>
                {
                    StatusCode = Domain.Enum.StatusCode.NotFound,
                    Description = "Категорий нет"
                };
            }
            return new BaseResponse<List<Category>>
            {
                Data = categories,
                StatusCode = Domain.Enum.StatusCode.OK
            };
        }
    }
}
