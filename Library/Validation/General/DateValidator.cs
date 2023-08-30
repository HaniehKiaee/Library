using Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Library.Validation.General
{
    public class DateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                StringConst.DateFormat, CultureInfo.CurrentCulture,
            DateTimeStyles.None,
            out dateTime);

            return isValid;
        }
    }
}
