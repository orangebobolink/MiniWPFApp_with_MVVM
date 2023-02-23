using BLL.RepositoryServices.Interfaces.RepositoryServices;
using DAL.Domain.Interfaces;
using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;

namespace BLL.RepositoryServices.Implementations.RepositoryServices
{
    public class TrophyRepositoryService : MainRepositoryService<Trophy>, ITrophyRepositoryService
    {
        public TrophyRepositoryService(ITrophyRepository repository) : base(repository)
        {
        }
    }
}
