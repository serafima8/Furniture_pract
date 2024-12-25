using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_LebedevaS.Domain.Enum;

namespace Furniture_LebedevaS.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string PathImg { get; set; }
        public int Star { get; set; }
        public Guid Categorie_id { get; set; }
    }
}
