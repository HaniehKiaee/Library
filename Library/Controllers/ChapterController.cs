using AutoMapper;
using Infrastructure.Data.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models.ChapterDtos;
using Domain;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Service.Paging;
using Infrastructure.Data.Service.Sorting;
using Newtonsoft.Json;
using Catalog.Extensions;
using Common.Constants;

namespace Library.Controllers
{
    [ApiController]
    public class ChapterController : BaseController
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public ChapterController(IChapterRepository chapterRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _chapterRepository = chapterRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Chapter.GetChapters)]
        public ActionResult<IEnumerable<ChapterSummaryDto>> GetChapters(int bookId)
        {
            var chapters = _chapterRepository.GetByBookId(bookId);
            var result = _mapper.Map<IEnumerable<ChapterSummaryDto>>(chapters);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Chapter.GetChapter)]
        public async Task<ActionResult<ChapterSummaryDto>> GetChapter(int chapterId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
            {
                return NotFound(Messages.ChapterNotFound);
            }
            var result = _mapper.Map<ChapterSummaryDto>(chapter);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Chapter.CustomGet)]
        public async Task<ActionResult<IEnumerable<ChapterSummaryDto>>> CustomGet([FromQuery] CustomGetChapterDto dto)
        {
            var FilterExpresison = this.BuildFilter(dto);
            var paging = _mapper.Map<PagingParam>(dto);
            var sorting = SortingParam<Chapter>.AsSorting(dto.Sorting);
            var chapters = await _chapterRepository.CustomGet(FilterExpresison, dto.SearchText, paging, sorting);

            var chapterDto = new List<ChapterSummaryDto>();
            foreach (var item in chapters)
            {
                chapterDto.Add(_mapper.Map<ChapterSummaryDto>(item));
            }
            var result = new PagedList<ChapterSummaryDto>(chapterDto, paging.PageIndex, paging.PageSize, chapters.Count);

            Response.Headers.Add(StringConst.X_Paging, JsonConvert.SerializeObject(chapters.pagingData));
            return Ok(result);
        }


        [HttpPost(ApiRoutes.Chapter.AddChapter)]
        public async Task<ActionResult<ChapterSummaryDto>> AddChapter(int bookId, [FromBody] CreateChapterDto chapterDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound(Messages.BookNotFound);
            }
            var chapter = _mapper.Map<Chapter>(chapterDto);
            chapter.BookId = bookId;
            await _chapterRepository.Add(chapter);
            var result = _mapper.Map<ChapterSummaryDto>(chapter);
            return Ok(result);
        }
        [HttpPut(ApiRoutes.Chapter.UpdateChapter)]
        public async Task<ActionResult<ChapterSummaryDto>> UpdateBook(int id, [FromBody] UpdateChapterDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookDto.BookId);
            if (book == null)
            {
                return NotFound(Messages.BookNotFound);
            }
            var chapter = await _chapterRepository.GetByIdAsync(id);
            if (chapter == null)
            {
                return BadRequest(Messages.ChapterNotFound);
            }
            chapter.Title = bookDto.Title;
            chapter.Total_pages = bookDto.Total_pages;
            chapter.Text = bookDto.Text;
            chapter.BookId = bookDto.BookId;
            try
            {
                await _chapterRepository.Update(chapter);
            }
            catch (DbUpdateConcurrencyException)
            {
                var checkExistBook = await _chapterRepository.GetByIdAsync(id);
                if (checkExistBook == null)
                    return BadRequest(Messages.ChapterRemoved);
            }

            var result = _mapper.Map<ChapterSummaryDto>(chapter);
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.Chapter.DeleteChapter)]
        public async Task<IActionResult> DeleteChapter(int chapterId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
            {
                return NotFound(Messages.ChapterNotFound);
            }
            await _chapterRepository.Remove(chapter);
            return Ok();
        }

        [HttpGet(ApiRoutes.Chapter.ChapterTextProcessing)]
        public async Task<IActionResult> ChapterTextProcessing(int chapterId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
            {
                return NotFound(Messages.ChapterNotFound);
            }
            var charFrequency = this.TextProcessing(chapter.Text);

            return Ok(charFrequency);
        }
    }
}
