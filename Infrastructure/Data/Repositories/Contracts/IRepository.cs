using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Core;
using Infrastructure.Data.Service.Paging;
using Infrastructure.Data.Service.Sorting;

namespace Infrastructure.Data.Repositories.Contracts
{
    public interface IRepository<T, key> where T : IEntity<key>
    {
        Task<T> GetByIdAsync(key key);
        IEnumerable<T> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        Task<PagedList<T>> CustomGet(Expression<Func<T, bool>> expression, string searchText, PagingParam pagingParam = default, SortingParam<T> sorting = default, bool tracking = default);
    }
}
