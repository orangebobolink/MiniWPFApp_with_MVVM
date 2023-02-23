using BLL.RepositoryServices.Interfaces.RepositoryServices;
using DAL.Domain.Interfaces;
using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;

namespace BLL.RepositoryServices.Implementations.RepositoryServices
{
    public class TypeAnimalRepositoryService : MainRepositoryService<TypeAnimal>, ITypeAnimalRepositoryService
    {
        public TypeAnimalRepositoryService(ITypeAnimalRepository repository) : base(repository)
        {
        }
    }
}
