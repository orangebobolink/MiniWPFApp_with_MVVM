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
    public class AnimalRepositoryService : IAnimalRepositoryService
    {
        private IAnimalRepository _repository;
        private IMapper _mapper;

        public AnimalRepositoryService(IAnimalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(AnimalDTO entity)
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

                var animal = _mapper.Map<Animal>(entity);
                await _repository.DeleteAsync(animal);

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

        public async Task<IBaseResponse<List<AnimalDTO>>> GetAllAsync()
        {
            try
            {
                var values = _mapper.Map<List<AnimalDTO>>(await _repository.GetAllAsync());

                return new BaseResponse<List<AnimalDTO>>()
                {
                    Data = values,
                    Description = "Data received successfully.",
                    StatusCode = new OkResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<AnimalDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<AnimalDTO>> GetValueAsync(int id)
        {
            try
            {
                var entity = _mapper.Map<AnimalDTO>(await _repository.GetValueAsync(id));

                if (entity == null)
                {
                    return new BaseResponse<AnimalDTO>()
                    {
                        StatusCode = new BadRequestResult(),
                        Description = "A value with this id doesn't exist"
                    };
                }

                return new BaseResponse<AnimalDTO>()
                {
                    Data = entity,
                    Description = "The value was successfully found.",
                    StatusCode = new BadRequestResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AnimalDTO>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateAsync(AnimalDTO entity)
        {
            try
            {
                var animal = _mapper.Map<Animal>(entity);
                await _repository.CreateAsync(animal);

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

        public async Task<IBaseResponse<bool>> UpdateAsync(AnimalDTO entity)
        {
            try
            {
                var animal = _mapper.Map<Animal>(entity);
                await _repository.UpdateAsync(animal);

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

        public async Task Save()
        {
            await _repository.Save();
        }
    }
}
