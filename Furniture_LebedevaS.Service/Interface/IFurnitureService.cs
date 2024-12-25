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

namespace Furniture_LebedevaS.Service.Interface
{
    public interface IFurnitureService 
    {
        BaseResponse<List<Product>> GetALLProductByIdCountry(Guid Id);
    }
}
