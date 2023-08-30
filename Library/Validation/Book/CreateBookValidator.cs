using Common.Constants;
using Library.Models.BookDtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Validation.Book
{
    public class CreateBookValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var createBookDto = value as CreateBookDto;
            if (createBookDto == null)
            {
                throw new InvalidCastException(Messages.InvalidDate);
            }
            return true;
        }
    }
}
