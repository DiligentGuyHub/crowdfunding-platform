using crowdfunding_application.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public class GenericService<T> : IGenericService<T> where T : ModelBase
    {
        protected readonly ApplicationDbContext _context;

        public GenericService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> Delete(int? id)
        {
            T entity = await _context.Set<T>().FirstOrDefaultAsync((entity) => entity.Id == id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<T> Update(int id, T entity)
        {
            entity.Id = id;

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<T> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<T> Get(int? id)
        {
            T entity = await _context.Set<T>().FirstOrDefaultAsync((entity) => entity.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> entities = await _context.Set<T>().ToListAsync();
            return entities;
        }
        public async Task<IEnumerable<T>> GetJoin(Func<T, bool> func, params Expression<Func<T, object>>[] expressions)
        {
            return new List<T>(_context.Set<T>().Where(func));
        }
    }
}
