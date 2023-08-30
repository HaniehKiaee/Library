using System;
using Domain;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repositories.Core;
using Infrastructure.Data.Repositories.Contracts;

using Common.Constants;
using Infrastructure.Data.Service.Paging;


using Infrastructure.Data.Service.Sorting;

namespace Infrastructure.Data.Repositories
{
    public class BookRepository : Repository<Book, int>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {

        }

        public override IEnumerable<Book> GetAll()
        {
            return _context.Books.Include(StringConst.Chapters).AsEnumerable();
        }

        public override async Task<Book> GetByIdAsync(int key)
        {
            return await _context.Books.Include(StringConst.Chapters).SingleOrDefaultAsync(p => p.Id == key);
        }

        public override async Task<PagedList<Book>> CustomGet(Expression<Func<Book, bool>> expression, string searchText, PagingParam paging, SortingParam<Book> sorting, bool tracking = false)
        {
            //return base.CustomGet(expression, searchText, paging, sorting, tracking);
            return await _context.Set<Book>().Tracking(tracking).
                                  Include(StringConst.Chapters).
                                  Where(expression).
                                  Searching(searchText).
                                  AsSorting(sorting).
                                  PagingAsync(paging);
        }

        public override async Task Remove(Book entity)
        {
            var bookAndRelatedChapters =
               await _context.Books.Include(book => book.Chapters)
                                    .FirstOrDefaultAsync(book => book.Id == entity.Id);
            if (bookAndRelatedChapters != null)
            {
                _context.Remove(bookAndRelatedChapters);

                await _context.SaveChangesAsync();
            }
        }
    }
}
