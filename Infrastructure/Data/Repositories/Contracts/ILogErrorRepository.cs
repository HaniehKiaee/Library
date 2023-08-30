using Domain;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Contracts
{
    public interface ILogErrorRepository 
    {
        Task LogError(Error error);
    }
}
