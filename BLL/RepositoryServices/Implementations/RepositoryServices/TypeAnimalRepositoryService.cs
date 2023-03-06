using AutoMapper;
using BLL.DTO;
using BLL.RepositoryServices.Interfaces.RepositoryServices;
using DAL.Domain;
using DAL.Domain.Interfaces;
using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLL.RepositoryServices.Implementations.RepositoryServices
{
    public class TypeAnimalRepositoryService : ITypeAnimalRepositoryService
    {
        private ITypeAnimalRepository _repository;
        private IMapper _mapper;

        public TypeAnimalRepositoryService(ITypeAnimalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(TypeAnimalDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = new NotFoundResult(),
                        Description = "Null object"
                    };
                }

                var typeAnimal = _mapper.Map<TypeAnimal>(entity);
                await _repository.DeleteAsync(typeAnimal);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = new OkResult(),
                    Description = "Ok result"
                };
            }
            catch
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = new BadRequestResult(),
                    Description = "Inevitable error"
                };
            }
        }

        public async Task<IBaseResponse<List<TypeAnimalDTO>>> GetAllAsync()
        {
            try
            {
                var values = _mapper.Map<List<TypeAnimalDTO>>(await _repository.GetAllAsync());

                return new BaseResponse<List<TypeAnimalDTO>>()
                {
                    Data = values,
                    Description = "Data received successfully.",
                    StatusCode = new OkResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<TypeAnimalDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<TypeAnimalDTO>> GetValueAsync(int id)
        {
            try
            {
                var entity = _mapper.Map<TypeAnimalDTO>(await _repository.GetValueAsync(id));

                if (entity == null)
                {
                    return new BaseResponse<TypeAnimalDTO>()
                    {
                        StatusCode = new BadRequestResult(),
                        Description = "A value with this id doesn't exist"
                    };
                }

                return new BaseResponse<TypeAnimalDTO>()
                {
                    Data = entity,
                    Description = "The value was successfully found.",
                    StatusCode = new BadRequestResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypeAnimalDTO>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateAsync(TypeAnimalDTO entity)
        {
            try
            {
                var typeAnimal = _mapper.Map<TypeAnimal>(entity);
                await _repository.CreateAsync(typeAnimal);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "The value was successfully added.",
                    StatusCode = new OkResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<bool>> UpdateAsync(TypeAnimalDTO entity)
        {
            try
            {
                var typeAnimal = _mapper.Map<TypeAnimal>(entity);
                await _repository.UpdateAsync(typeAnimal);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "The value information was successfully updates.",
                    StatusCode = new OkResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }
    }
}
