using Infrastructure.Data.Service.Paging;
using System;

namespace Library.Models.ChapterDtos
{
    public class CustomGetChapterDto
    {
        //Filter data
        public int? Min_Total_pages { get; set; }
        public int? Max_Total_pages { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        //Search data
        public string SearchText { get; set; }

        //Paging data
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }

        //Sorting data
        public string Sorting { get; set; }

        public CustomGetChapterDto()
        {
            PageSize = PagingParam.DefaultPageSize;
            PageIndex = 1;
        }
    }
}
