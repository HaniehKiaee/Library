using AutoMapper;
using Domain;
using Infrastructure.Data.Service.Paging;
using Library.Models.BookDtos;
using System.Collections.Generic;

namespace Library.Mapping
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookSummaryDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<CustomGetBookDto, PagingParam>();
        }
    }
}
