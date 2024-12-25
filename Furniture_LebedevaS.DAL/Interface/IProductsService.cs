
using Furniture_LebedevaS.Domain.Enum;
using Furniture_LebedevaS.Domain.Models;
using Furniture_LebedevaS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.DAL.Interface
{
    public interface IProductsService 
    {
        BaseResponse<List<Product>> GetAllProducts();
        BaseResponse<List<Product>> GetALLProductByCategoryId(Guid Id);
    }
    
 
}
