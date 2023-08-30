using Common.Constants;
using Library.Models.BookDtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Validation.Book
{
    public class UpdateBookValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var updateBookDto = value as UpdateBookDto;
            if (updateBookDto == null)
            {
                throw new InvalidCastException(Messages.InvalidDate);
            }
            return true;
        }
    }
}
