using Common.Constants;
using Infrastructure.Data.Service.Sorting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Service.Paging
{
    public static class DbServiceExtension
    {
        public static async Task<PagedList<T>> PagingAsync<T>(this IQueryable<T> query, PagingParam? paging)
        {
            if (paging == null)
            {
                paging = new PagingParam();
            }

            var items = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var recordCount = await query.CountAsync();
            return new PagedList<T>(items, paging.PageIndex, paging.PageSize, recordCount);
        }

        public static IQueryable<T> AsSorting<T>(this IQueryable<T> query, SortingParam<T>? sorting = default)
        {
            if (sorting == null)
            {
                return query;
            }

            for (int i = 0; i < sorting.Sorts.Count; i++)
            {
                if (i == 0)
                {
                    var first = sorting.Sorts.First();
                    switch (first.Direction)
                    {
                        case SortDirection.Ascending:
                            query = query.OrderBy(first.Field);
                            break;
                        case SortDirection.Descending:
                            query = query.OrderByDescending(first.Field);
                            break;
                    }
                }
                else
                {
                    var sort = sorting.Sorts.ToArray()[i];
                    switch (sort.Direction)
                    {
                        case SortDirection.Ascending:
                            query = (query as IOrderedQueryable<T>)!.ThenBy(sort.Field);
                            break;
                        case SortDirection.Descending:
                            query = (query as IOrderedQueryable<T>)!.ThenByDescending(sort.Field);
                            break;
                    }
                }
            }
            return query;
        }

        public static IQueryable<T> Searching<T>(this IQueryable<T> query, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return query;

            var type = typeof(T);

            BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase;
            var properties = type.GetProperties(DefaultLookup)

            //var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(pi => pi.PropertyType.Name.Equals(StringConst.StringType)).ToList();
            if (!properties.Any())
                return query;
            var exps = new List<Expression>();
            var parameter = Expression.Parameter(type);
            var mi = typeof(string).GetMethod(StringConst.Contains, new[] { typeof(string) });
            var ce = Expression.Constant(searchText, typeof(string));
            properties.ForEach(props =>
            {
                var me = Expression.Property(parameter, props);
                var mce = Expression.Call(me, mi, ce);
                exps.Add(mce);
            });

            if (exps.Count == 1)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(exps.First(), new[] { parameter });
                return query.Where(lambda);
            }
            else
            {
                var be = Expression.Or(exps[0], exps[1]);
                for (int i = 2; i < exps.Count; i++)
                {
                    be = Expression.Or(exps[i], be);
                }
                var lambda = Expression.Lambda<Func<T, bool>>(be, new[] { parameter });
                return query.Where(lambda);
            }
        }

        public static IQueryable<T> Tracking<T>(this IQueryable<T> query, bool tracking) 
            where T: class
        {
            return tracking ? query : query.AsNoTracking();
        }
    }
}
