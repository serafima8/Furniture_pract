using Furniture_LebedevaS.Domain;
using Furniture_LebedevaS.Domain.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Furniture_LebedevaS.Domain.Models;
using Furniture_LebedevaS.Service.LoginAndRegistration;
using Furniture_LebedevaS.Domain.ViewModels.Categories;
using Furniture_LebedevaS.Domain.ViewModels.Products;

namespace Furniture_LebedevaS.Service
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            // Пользователи
            CreateMap<User, UserDb>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<RegisterViewModel, ConfirmEmailViewModel>().ReverseMap();
            CreateMap<User, ConfirmEmailViewModel>().ReverseMap();

            // Категории товаров
            CreateMap<Category, CategoryDb>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();


            // Продукты
            CreateMap<Product, ProductForListOfProductsViewModel>().ReverseMap();

            CreateMap<Product, ProductDb>().ReverseMap();

        }

    }
}
