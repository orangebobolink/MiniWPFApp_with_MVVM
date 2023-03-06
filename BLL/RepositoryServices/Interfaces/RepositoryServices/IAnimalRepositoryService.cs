using BLL.DTO;
using DAL.Domain.Interfaces;
using DAL.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Domain.Interfaces.ImplementationsOfRepository;

namespace BLL.RepositoryServices.Interfaces.RepositoryServices
{
    public interface IAnimalRepositoryService : IRepositoryService<AnimalDTO>
    {

    }
}
