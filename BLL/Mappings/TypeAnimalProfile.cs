using AutoMapper;
using BLL.DTO;
using DAL.Domain.Models;

namespace BLL.Mappings
{
    internal class TypeAnimalProfile : Profile
    {
        public TypeAnimalProfile()
        {
            CreateMap<TypeAnimal, TypeAnimalDTO>().ReverseMap();
        }
    }
}
