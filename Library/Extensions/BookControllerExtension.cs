using Common.Constants;
using Domain;
using Library.Controllers;
using Library.Models.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Catalog.Extensions
{
    public static class BookControllerExtension
    {
        public static Expression<Func<Book, bool>> BuildFilter(this BookController bookController, CustomGetBookDto filterDto)
        {
            List<Expression> expressions = new List<Expression>();
            ParameterExpression peBook = Expression.Parameter(typeof(Book), StringConst.B);
            if (filterDto.Min_Total_pages.HasValue || filterDto.Max_Total_pages.HasValue)
            {
                MemberExpression meTotalPages = Expression.Property(peBook, nameof(Book.Total_pages));
                if (filterDto.Min_Total_pages.HasValue)
                {
                    ConstantExpression ceMinTotalPages = Expression.Constant(filterDto.Min_Total_pages, typeof(int));
                    BinaryExpression beTotalPagesGreaterThanOrEqual = Expression.GreaterThanOrEqual(meTotalPages, ceMinTotalPages);

                    expressions.Add(beTotalPagesGreaterThanOrEqual);
                }
                if (filterDto.Max_Total_pages.HasValue)
                {
                    ConstantExpression ceMaxTotalPages = Expression.Constant(filterDto.Max_Total_pages, typeof(int));
                    BinaryExpression beTotalPagesLessThanOrEqual = Expression.LessThanOrEqual(meTotalPages, ceMaxTotalPages);
                    expressions.Add(beTotalPagesLessThanOrEqual);
                }
            }
            if (!string.IsNullOrEmpty(filterDto.Title))
            {
                MemberExpression meTitle = Expression.Property(peBook, nameof(Book.Title));
                ConstantExpression ceTitle = Expression.Constant(filterDto.Title, typeof(string));
                MethodInfo miContains = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                MethodCallExpression mceContains = Expression.Call(meTitle, miContains, ceTitle);
                expressions.Add(mceContains);
            }
            if (!expressions.Any())
            {
                return p => true;
            }
            else if (expressions.Count == 1)
            {
                return Expression.Lambda<Func<Book, bool>>(expressions[0], new[] { peBook });
            }
            else
            {
                BinaryExpression beAnd = Expression.And(expressions[0], expressions[1]);
                for (int i = 2; i < expressions.Count; i++)
                {
                    beAnd = Expression.And(beAnd, expressions[i]);
                }
                return Expression.Lambda<Func<Book, bool>>(beAnd, new[] { peBook });
            }
        }
    }
}
