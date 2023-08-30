using Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repositories.Core;
using Infrastructure.Data.Repositories.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public class LogInformationRepository : ILogInformationRepository
    {
        private readonly LibraryContext _context;
        public LogInformationRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task LogInformation(Information information)
        {
            await _context.Information.AddAsync(information);
            await _context.SaveChangesAsync();
        }
    }
}
