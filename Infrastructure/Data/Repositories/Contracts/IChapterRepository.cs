using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Contracts
{
    public interface IChapterRepository : IRepository<Chapter, int>
    {
        IEnumerable<Chapter> GetByBookId(int key);
    }
}
