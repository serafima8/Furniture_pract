using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_LebedevaS.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture_LebedevaS.Domain.ModelsDb
{
    [Table("user_data")]
    public class UserDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("roler")]
        public Role Role { get; set; }

        [Column("pathImage")]
        public string PathImage { get; set; }
    }
    
}
