using System;
using System.Collections.Generic;
using Furniture_LebedevaS.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Furniture_LebedevaS.Domain.ModelsDb
{
    [Table("order")]
   public class OrderDb
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("user_id")]
        public Guid User_id { get; set; }
        [Column("product_id")]
        public Guid Product_id { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
    }
}
