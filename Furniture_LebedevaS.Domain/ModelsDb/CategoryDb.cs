using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_LebedevaS.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture_LebedevaS.Domain.ModelsDb
{
    [Table("categories")]
    public class CategoryDb
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("pathImage")]
        public string? PathImage { get; set; }
        List<ProductDb> Products { get; set; }

    }
}
