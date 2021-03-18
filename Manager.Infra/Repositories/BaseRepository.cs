using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly ManagerContext _context;

        public BaseRepository(ManagerContext context)
        {
            _context = context; 
        }
        public virtual async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<T> Get(int id)
        {
            var obj = await _context.Set<T>()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return obj;
        }

        public virtual async Task<List<T>> GetList()
        {
            return await _context.Set<T>()
                .AsNoTracking()                
                .ToListAsync();           
        }

       

        public virtual async Task Remove(int id)
        {
            var user = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> Update(T obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
