using Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repositories.Core;
using Infrastructure.Data.Repositories.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public class LogErrorRepository : ILogErrorRepository
    {
        private readonly LibraryContext _context;
        public LogErrorRepository(LibraryContext context) 
        {
            _context = context;
        }

        public async Task LogError(Error error)
        {
           await _context.Errors.AddAsync(error);
           await _context.SaveChangesAsync();
        }
    }
}
