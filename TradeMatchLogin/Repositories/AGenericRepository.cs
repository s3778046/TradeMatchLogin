using TradeMatchLogin.Repositories.Interface;
using TradeMatchLogin.DataContext;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System.Linq;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.Repositories
{
    public abstract class AGenericRepository<Entity, Key> : IGenericRepository<Entity, Key> where Entity : class
    {
        protected TradeMatchContext _context;

        public AGenericRepository(TradeMatchContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>().ToListAsync();
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<Entity> DeleteAsync(Key key)
        {
            var entity= await _context.Set<Entity>().FindAsync(key);
            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<Entity> FindAsync(Key key)
        {

            var entity = await _context.Set<Entity>().FindAsync(key);

            return entity;
        }

        public virtual async Task<Entity> UpdateAsync(Key key, Entity entity)
        {
           
            _context.Set<Entity>().Update(entity);
           await _context.SaveChangesAsync();

            return entity;
        }
    }
}
