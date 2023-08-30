using Domain;
using AutoMapper;
using Newtonsoft.Json;
using Common.Constants;
using Catalog.Extensions;
using System.Threading.Tasks;
using Library.Models.BookDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Service.Paging;
using Infrastructure.Data.Service.Sorting;
using Infrastructure.Data.Repositories.Contracts;

namespace Library.Controllers
{
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Book.GetBooks)]
        public ActionResult<IEnumerable<BookSummaryDto>> GetBooks()
        {
            var books = _bookRepository.GetAll();
            var result = _mapper.Map<IEnumerable<BookSummaryDto>>(books);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Book.GetBook)]
        public async Task<ActionResult<BookSummaryDto>> GetBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(Messages.BookNotFound);
            }
            var result = _mapper.Map<BookSummaryDto>(book);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Book.CustomGet)]
        public async Task<ActionResult<IEnumerable<BookSummaryDto>>> CustomGet([FromQuery] CustomGetBookDto dto)
        {
            var FilterExpresison = this.BuildFilter(dto);
            var paging = _mapper.Map<PagingParam>(dto);
            var sorting = SortingParam<Book>.AsSorting(dto.Sorting);
            var books = await _bookRepository.CustomGet(FilterExpresison,dto.SearchText, paging, sorting);
            
            var bookDto = new List<BookSummaryDto>();
            foreach (var item in books)
            {
                bookDto.Add(_mapper.Map<BookSummaryDto>(item));
            }
            var result = new PagedList<BookSummaryDto>(bookDto, paging.PageIndex,paging.PageSize,books.Count);

            Response.Headers.Add(Common.Constants.StringConst.X_Paging, JsonConvert.SerializeObject(books.pagingData));
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Book.AddBook)]
        public async Task<ActionResult<BookSummaryDto>> AddBook([FromBody] CreateBookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.Add(book);
            var result = _mapper.Map<BookSummaryDto>(book);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.Book.UpdateBook)]
        public async Task<ActionResult<BookSummaryDto>> UpdateBook(int id, [FromBody] UpdateBookDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return BadRequest(Messages.BookNotFound);
            }
            book.Title = bookDto.Title;
            book.Total_pages = bookDto.Total_pages;
            book.Published_Date = bookDto.Published_Date;
            try
            {
                await _bookRepository.Update(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                var checkExistBook = await _bookRepository.GetByIdAsync(id);
                if (checkExistBook == null)
                    return BadRequest(Messages.BookRemoved);
            }

            var result = _mapper.Map<BookSummaryDto>(book);
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.Book.DeleteBook)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(Messages.BookNotFound);
            }
            await _bookRepository.Remove(book);
            return Ok();
        }
    }
}
