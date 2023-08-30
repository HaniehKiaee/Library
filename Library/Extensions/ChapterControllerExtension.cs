using Common.Constants;
using Domain;
using Library.Controllers;
using Library.Models.ChapterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Catalog.Extensions
{
    public static class ChapterControllerExtension
    {
        public static Expression<Func<Chapter, bool>> BuildFilter(this ChapterController chapterController, CustomGetChapterDto filterDto)
        {
            List<Expression> expressions = new List<Expression>();
            ParameterExpression peChapter = Expression.Parameter(typeof(Chapter), StringConst.B);
            if (filterDto.Min_Total_pages.HasValue || filterDto.Max_Total_pages.HasValue)
            {
                MemberExpression meTotalPages = Expression.Property(peChapter, nameof(Chapter.Total_pages));
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
                MemberExpression meTitle = Expression.Property(peChapter, nameof(Chapter.Title));
                ConstantExpression ceTitle = Expression.Constant(filterDto.Title, typeof(string));
                MethodInfo miContains = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                MethodCallExpression mceContains = Expression.Call(meTitle, miContains, ceTitle);
                expressions.Add(mceContains);
            }
            if (!string.IsNullOrEmpty(filterDto.Text))
            {
                MemberExpression meText = Expression.Property(peChapter, nameof(Chapter.Text));
                ConstantExpression ceText = Expression.Constant(filterDto.Text, typeof(string));
                MethodInfo miContains = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                MethodCallExpression mceContains = Expression.Call(meText, miContains, ceText);
                expressions.Add(mceContains);
            }
            if (!expressions.Any())
            {
                return p => true;
            }
            else if (expressions.Count == 1)
            {
                return Expression.Lambda<Func<Chapter, bool>>(expressions[0], new[] { peChapter });
            }
            else
            {
                BinaryExpression beAnd = Expression.And(expressions[0], expressions[1]);
                for (int i = 2; i < expressions.Count; i++)
                {
                    beAnd = Expression.And(beAnd, expressions[i]);
                }
                return Expression.Lambda<Func<Chapter, bool>>(beAnd, new[] { peChapter });
            }
        }

        public static string TextProcessing(this ChapterController chapterController, string text)
        {
            char character = ' ';
            char temp;
            int freq = 0;
            string finalText = "";
            for (int i = 0; i < text.Length; i++)
            {
                finalText = finalText.Insert(finalText.Length, text[i].ToString());

                if (character == text[i] && character != text[i + 1])
                {
                    temp = text[i + 1];

                    for (int j = i + 1; j < text.Length; j++)
                    {
                        if (text[j] == temp)
                            freq++;
                    }

                    if (freq > 0)
                    {
                      string   val = " " + freq;
                        finalText = finalText.Insert(finalText.Length, val);
                        freq = 0;
                    }
                }
            }

            return finalText;
        }
    }
}
