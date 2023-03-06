using AutoMapper;
using BLL.DTO;
using DAL.Domain.Models;

namespace BLL.Mappings
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
