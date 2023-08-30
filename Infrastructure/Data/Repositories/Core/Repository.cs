using Domain.Core;
using Infrastructure.Data.Repositories.Contracts;
using Infrastructure.Data.Service.Paging;
using Infrastructure.Data.Service.Sorting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Core
{
    public class Repository<T, Key> : IRepository<T, Key>
        where T : Entity<Key>
    {
        protected readonly LibraryContext _context;
        protected readonly DbSet<T> _set;
        public Repository(LibraryContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _set.AsEnumerable();
        }

        public virtual async Task<T> GetByIdAsync(Key key)
        {
            return await _set.FindAsync(key);
        }

        public async Task Add(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<PagedList<T>> CustomGet(Expression<Func<T, bool>> expression, string searchText, PagingParam paging, SortingParam<T> sorting, bool tracking = false)
        {
            return await _set.Tracking(tracking).
                Where(expression).
                Searching(searchText).
                AsSorting(sorting).
                PagingAsync(paging);
        }

        public virtual async Task Remove(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
