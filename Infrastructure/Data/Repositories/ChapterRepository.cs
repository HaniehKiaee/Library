using Domain;
using Infrastructure.Data.Repositories.Core;
using Infrastructure.Data.Repositories.Contracts;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ChapterRepository : Repository<Chapter, int>, IChapterRepository
    {
        public ChapterRepository(LibraryContext context) : base(context)
        {

        }

        public  IEnumerable<Chapter> GetByBookId(int key)
        {
            return  _context.Chapters.Where(chapter => chapter.BookId == key).AsEnumerable();
        }
    }
}
