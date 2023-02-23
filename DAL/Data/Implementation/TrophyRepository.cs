using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;

namespace DAL.Data.Implementation
{
    internal class TrophyRepository : MainEFReposiotry<Trophy>, ITrophyRepository
    {
        public TrophyRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
