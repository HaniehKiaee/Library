using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Constants
{
    public static class ApiRoutes
    {
        public const string Swagger_Path = "/swagger/";
        public const string BaseApiUrl = "api";
        public const string BaseRoute = "api/[controller]";
        public static class Book
        {
            private const string Base = BaseApiUrl + "/Book";
            public const string GetBooks = Base + "/GetBooks";
            public const string GetBook = Base + "/GetBook";
            public const string CustomGet = Base + "/CustomGet";
            public const string AddBook = Base + "/AddBook";
            public const string UpdateBook = Base + "/UpdateBook";
            public const string DeleteBook = Base + "/DeleteBook";
        }
        public static class Chapter
        {
            private const string Base = BaseApiUrl + "/Chapter";
            public const string GetChapters = Base + "/GetChapters";
            public const string GetChapter = Base + "/GetChapter";
            public const string CustomGet = Base + "/CustomGet";
            public const string AddChapter = Base + "/AddChapter";
            public const string UpdateChapter = Base + "/UpdateChapter";
            public const string DeleteChapter = Base + "/DeleteChapter";
            public const string ChapterTextProcessing = Base + "/ChapterTextProcessing";
        }
    }
}
