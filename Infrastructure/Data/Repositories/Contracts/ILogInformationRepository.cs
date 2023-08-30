using Domain;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Contracts
{
    public interface ILogInformationRepository
    {
        Task LogInformation(Information information);
    }
}
