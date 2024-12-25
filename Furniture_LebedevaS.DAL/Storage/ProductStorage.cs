using Furniture_LebedevaS.DAL.Interface;
using Furniture_LebedevaS.Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.DAL.Storage
{
    public class ProductStorage : IBaseStorage<ProductDb>
    {
        private readonly ApplicationDbContext _db;

        public ProductStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(ProductDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(ProductDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<ProductDb> Get(Guid id)
        {
            return await _db.ProductsDb.FirstOrDefaultAsync(x => x.Id == id);
        }


        public IQueryable<ProductDb> GetAll()
        {
            return _db.ProductsDb;
        }

        public async Task<ProductDb> Update(ProductDb item)
        {
            _db.ProductsDb.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
