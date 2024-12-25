using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_LebedevaS.Domain.Enum;

namespace Furniture_LebedevaS.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid User_id { get; set; }
        public Guid Product_id { get; set; }
        public DateTime Date { get; set; }
    }
}
