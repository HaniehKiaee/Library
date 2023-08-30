using Common.Constants;
using Library.Models.ChapterDtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Validation.Chapter
{
    public class CreateChapterValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var createChapterDto = value as CreateChapterDto;
            if (createChapterDto == null)
            {
                throw new InvalidCastException(Messages.InvalidDate);
            }
            return true;
        }
    }
}
