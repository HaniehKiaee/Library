using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Service.Paging
{
    public class PagingData
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(RecordCount / (double)PageSize);
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagingData(int currentPage,int pageSize,int recordCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            RecordCount = recordCount;
        }
    }
}
