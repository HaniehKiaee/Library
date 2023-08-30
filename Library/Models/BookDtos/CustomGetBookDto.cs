using Infrastructure.Data.Service.Paging;
using System;

namespace Library.Models.BookDtos
{
    public class CustomGetBookDto
    {
        //Filter data
        public int? Min_Total_pages { get; set; }
        public int? Max_Total_pages { get; set; }
        public string Title { get; set; }

        //Search data
        public string SearchText { get; set; }

        //Paging data
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }

        //Sorting data
        public string Sorting { get; set; }

        public CustomGetBookDto()
        {
            PageSize = PagingParam.DefaultPageSize;
            PageIndex = 1;
        }
    }
}
