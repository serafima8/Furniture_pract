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
    public class CategoryStorage : IBaseStorage<CategoryDb>
    {
        private readonly ApplicationDbContext _db;

        public CategoryStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(CategoryDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(CategoryDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<CategoryDb> Get(Guid id)
        {
            return await _db.CategoriessDb.FirstOrDefaultAsync(x => x.Id == id);
        }


        public IQueryable<CategoryDb> GetAll()
        {
            return _db.CategoriessDb;
        }

        public async Task<CategoryDb> Update(CategoryDb item)
        {
            _db.CategoriessDb.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
