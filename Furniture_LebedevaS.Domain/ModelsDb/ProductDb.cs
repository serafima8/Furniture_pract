using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_LebedevaS.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture_LebedevaS.Domain.ModelsDb
{
    [Table("product")]
    public class ProductDb
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("stars")]
        public int? Star {  get; set; }

        [Column("category_id")]
        public Guid CategorieId { get; set; }

        [Column("pathImg")]
        public string PathImg { get; set; }

        [ForeignKey("CategorieId")]
        public CategoryDb Categories { get; set; }


        //public DateTime CreatedAt { get; set; }

    }
}
