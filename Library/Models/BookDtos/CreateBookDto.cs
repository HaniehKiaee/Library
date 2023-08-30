using Library.Validation.Book;
using Library.Validation.General;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.BookDtos
{
    [CreateBookValidator]
    public class CreateBookDto
    {

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }
        [Required]
        [Range(1, 10000000000)]
        public int Total_pages { get; set; }
        [Required]
        [StringLength(8)]
        [DateValidator]
        public string Published_Date { get; set; }
    }
}
