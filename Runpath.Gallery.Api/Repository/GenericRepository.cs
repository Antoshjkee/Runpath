using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Runpath.Gallery.Domain;

namespace Runpath.Gallery.Api.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public GenericRepository(GalleryContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual async void AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<T> enitities)
        {
            _context.Set<T>().AddRange(enitities);
        }

        public virtual async void AddRangeAsync(IEnumerable<T> enitities)
        {
            await _context.Set<T>().AddRangeAsync(enitities);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
