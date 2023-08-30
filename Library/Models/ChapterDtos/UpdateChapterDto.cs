using Library.Validation.Chapter;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.ChapterDtos
{
    [UpdateChapterValidator]
    public class UpdateChapterDto
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }
        [Required]
        [Range(1, 10000000000)]
        public int Total_pages { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
