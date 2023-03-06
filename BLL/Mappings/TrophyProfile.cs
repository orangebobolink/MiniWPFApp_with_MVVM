using AutoMapper;
using BLL.DTO;
using DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappings
{
    internal class TrophyProfile : Profile
    {
        public TrophyProfile()
        {
            CreateMap<Trophy, TrophyDTO>().ReverseMap();
        }
    }
}
