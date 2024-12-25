using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Furniture_LebedevaS.Domain.ModelsDb;

namespace Furniture_LebedevaS.DAL
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<UserDb> UsersDb { get; set; }
        public DbSet<ProductDb> ProductsDb { get; set; }
        public DbSet<OrderDb> OrdersDb { get; set; }
        public DbSet <CategoryDb> CategoriessDb { get; set; }

        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) { }
    }
}
