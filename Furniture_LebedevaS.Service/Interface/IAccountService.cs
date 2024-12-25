using Furniture_LebedevaS.Domain.Models;
using Furniture_LebedevaS.Domain.Response;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Service.Interface
{
    public interface IAccountService
    {
       public Task<BaseResponse<string>> Register(User model);
       public Task<BaseResponse<ClaimsIdentity>> Login(User model);
        public Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode);
        public Task <BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model);
    }
}
