using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_LebedevaS.Domain.Enum;

namespace Furniture_LebedevaS.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PathImage { get; set; }
    }
}
