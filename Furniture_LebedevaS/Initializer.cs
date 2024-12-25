using Furniture_LebedevaS.DAL.Interface;
using Furniture_LebedevaS.Domain.ModelsDb;
using Furniture_LebedevaS.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Furniture_LebedevaS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_LebedevaS.DAL.Storage;
using Furniture_LebedevaS.Service.Realizations;

namespace Furniture_LebedevaS.Service
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
            services.AddScoped<IBaseStorage<ProductDb>, ProductStorage>();
            services.AddScoped<IBaseStorage<CategoryDb>, CategoryStorage>();
        }
        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductsService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddControllersWithViews()
           .AddDataAnnotationsLocalization()
           .AddViewLocalization();
        }
    }
}
