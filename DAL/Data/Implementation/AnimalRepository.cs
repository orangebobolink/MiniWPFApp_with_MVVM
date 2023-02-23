using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;

namespace DAL.Data.Implementation
{
    internal class AnimalRepository : MainEFReposiotry<Animal>, IAnimalRepository
    {
        public AnimalRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
