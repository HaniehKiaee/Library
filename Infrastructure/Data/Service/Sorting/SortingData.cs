using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Data.Service.Sorting
{
    public class SortingData<T>
    {
        public Expression<Func<T, object>> Field { get; set; }
        public SortDirection Direction { get; set; }
    }
}
