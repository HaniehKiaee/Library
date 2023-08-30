using AutoMapper;
using Domain;
using Infrastructure.Data.Service.Paging;
using Library.Models.ChapterDtos;

namespace Library.Mapping
{
    public class ChapterProfile : Profile
    {
        public ChapterProfile()
        {
            CreateMap<Chapter, ChapterSummaryDto>();
            CreateMap<CreateChapterDto, Chapter>();
            CreateMap<CustomGetChapterDto, PagingParam>();
        }
    }
}
