using AutoMapper;
using BLL.DTO;
using BLL.RepositoryServices.Interfaces.RepositoryServices;
using DAL.Domain.Interfaces;
using DAL.Domain;
using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BLL.RepositoryServices.Implementations.RepositoryServices
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private IUserRepository _repository;
        private IMapper _mapper;

        public UserRepositoryService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(UserDTO entity)
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

                var user = new User();
                _mapper.Map(entity, user);
                await _repository.DeleteAsync(user);

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

        public async Task<IBaseResponse<List<UserDTO>>> GetAllAsync()
        {
            try
            {
                var user = await _repository.GetAllAsync();
                //var s = new Lazy<IMapper>(_mapper);
                var values = _mapper.Map<List<UserDTO>>(user);

                return new BaseResponse<List<UserDTO>>()
                {
                    Data = values,
                    Description = "Data received successfully.",
                    StatusCode = new OkResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<UserDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<UserDTO>> GetValueAsync(int id)
        {
            try
            {
                var entity = _mapper.Map<UserDTO>(await _repository.GetValueAsync(id));

                if (entity == null)
                {
                    return new BaseResponse<UserDTO>()
                    {
                        StatusCode = new BadRequestResult(),
                        Description = "A value with this id doesn't exist"
                    };
                }

                return new BaseResponse<UserDTO>()
                {
                    Data = entity,
                    Description = "The value was successfully found.",
                    StatusCode = new BadRequestResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserDTO>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateAsync(UserDTO entity)
        {
            try
            {
                var user = new User();
                _mapper.Map(entity, user);
                await _repository.CreateAsync(user);

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

        public async Task<IBaseResponse<bool>> UpdateAsync(UserDTO entity)
        {
            try
            {
                var animal = _mapper.Map<User>(entity);
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
