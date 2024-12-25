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
    public class OrderStorage : IBaseStorage<OrderDb>
    {
        private readonly ApplicationDbContext _db;

        public OrderStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(OrderDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(OrderDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<OrderDb> Get(Guid id)
        {
            return await _db.OrdersDb.FirstOrDefaultAsync(x => x.Id == id);
        }


        public IQueryable<OrderDb> GetAll()
        {
            return _db.OrdersDb;
        }

        public async Task<OrderDb> Update(OrderDb item)
        {
            _db.OrdersDb.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
