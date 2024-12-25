using Furniture_LebedevaS.Domain.Models;
using Furniture_LebedevaS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.DAL.Interface
{
    public interface ICategoryService
    {
        BaseResponse<List<Category>> GetAllCategories();
    }
}
