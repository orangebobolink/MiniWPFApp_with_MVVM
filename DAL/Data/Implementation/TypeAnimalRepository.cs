using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;

namespace DAL.Data.Implementation
{
    internal class TypeAnimalRepository : MainEFReposiotry<TypeAnimal>, ITypeAnimalRepository
    {
        public TypeAnimalRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
