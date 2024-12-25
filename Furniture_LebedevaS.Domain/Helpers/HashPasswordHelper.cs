using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;
using Furniture_LebedevaS.Domain.Models;

namespace Furniture_LebedevaS.Domain.Helpers
{
    public  class HashPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256=SHA256.Create())
            {
                var hashedBytes=sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-","").ToLower();
                return hash;
            }
        }
    }

    public static class AuthenticateUserHelper
    {
        public static ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                new Claim("AvatarPath", user.PathImage),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
