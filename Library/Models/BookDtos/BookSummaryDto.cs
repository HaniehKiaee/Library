using Library.Models.ChapterDtos;
using System.Collections.Generic;

namespace Library.Models.BookDtos
{
    public class BookSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Total_pages { get; set; }
        public string Published_Date { get; set; }
        public List<ChapterSummaryDto> Chapters { get; set; }
    }
}
