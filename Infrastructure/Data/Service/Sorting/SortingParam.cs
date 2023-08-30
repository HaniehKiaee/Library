using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Service.Sorting
{
    public class SortingParam<T>
    {
        private List<SortingData<T>> _sorts;
        public IReadOnlyCollection<SortingData<T>> Sorts => _sorts.AsReadOnly();
        public SortingParam()
        {
            _sorts = new List<SortingData<T>>();
        }
        public void AddSort(Expression<Func<T, object>> field, SortDirection direction)
        {
            _sorts.Add(new SortingData<T> { Field = field, Direction = direction });
        }
        public static SortingParam<T> AsSorting(string sorting)
        {
            var sortingParam = new SortingParam<T>();
            if (string.IsNullOrEmpty(sorting))
            {
                return sortingParam;
            }
            var sorts = sorting.Split(StringConst.Comma);     //Total_pages asc,  Title desc

            sorts.ToList().ForEach(sorts =>
            {
                var sortData = sorts.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var propertyName = sortData[0];
                var propertyDirection = sortData[1].ToLower() == StringConst.asc ? SortDirection.Ascending : SortDirection.Descending;
                var tType = typeof(T);
                var pe = Expression.Parameter(tType, StringConst.B);
                BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase;
                var propertyInfo = tType.GetProperty(propertyName, DefaultLookup);
                if (propertyInfo == null)
                {
                    throw new InvalidOperationException(Messages.InvalidColumnName + propertyName);
                }
                var me = Expression.Property(pe, propertyInfo);
                var ce = Expression.Convert(me, typeof(object));
                var exp = Expression.Lambda<Func<T, object>>(ce, new[] { pe });
                sortingParam.AddSort(exp, propertyDirection);
            });
            return sortingParam;
        }
    }
}
