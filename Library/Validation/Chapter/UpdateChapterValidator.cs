using Common.Constants;
using Library.Models.ChapterDtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Validation.Chapter
{
    public class UpdateChapterValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var updateChapterDto = value as UpdateChapterDto;
            if (updateChapterDto == null)
            {
                throw new InvalidCastException(Messages.InvalidDate);
            }
            return true;
        }
    }
}
