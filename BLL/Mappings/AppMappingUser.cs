using AutoMapper;
using BLL.DTO;
using DAL.Domain.Models;

namespace BLL.Mappings
{
    internal class AppMappingUser : User
    {
        public AppMappingUser()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });

            IMapper mapper = config.CreateMapper();
        }
    }
}
