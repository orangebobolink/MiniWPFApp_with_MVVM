using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;

namespace DAL.Data.Implementation
{
    internal class UserRepository : MainEFReposiotry<User>, IUserRepository
    {
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
