using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Service.Paging
{
    public class PagedList<T> : List<T>
    {
        public PagingData pagingData { get; set; }
        public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int recordCount)
        {
            AddRange(items);    //Add items to the List
            pagingData = new PagingData(currentPage, pageSize, recordCount);
        }
    }
}
