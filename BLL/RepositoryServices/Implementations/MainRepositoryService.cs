using BLL.RepositoryServices.Interfaces;
using DAL.Domain.Interfaces;
using DAL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BLL.RepositoryServices.Implementations
{
    public abstract class MainRepositoryService<T>
        : IRepositoryService<T> where T : EntityBase
    {
        private readonly IRepository<T> _repository;

        protected MainRepositoryService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IBaseResponse<bool>> DeleteAsync(T entity)
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

                await _repository.DeleteAsync(entity);

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

        public virtual async Task<IBaseResponse<List<T>>> GetAllAsync()
        {
            try
            {
                List<T> values = await _repository.GetAllAsync();

                return new BaseResponse<List<T>>()
                {
                    Data = values,
                    Description = "Data received successfully.",
                    StatusCode = new OkResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<T>>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public virtual async Task<IBaseResponse<T>> GetValueAsync(int id)
        {
            try
            {
                var entity = (T?)await _repository.GetValueAsync(id);

                if (entity == null)
                {
                    return new BaseResponse<T>()
                    {
                        StatusCode = new BadRequestResult(),
                        Description = "A value with this id doesn't exist"
                    };
                }

                return new BaseResponse<T>()
                {
                    Data = entity,
                    Description = "The value was successfully found.",
                    StatusCode = new BadRequestResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public virtual async Task<IBaseResponse<bool>> CreateAsync(T entity)
        {
            try
            {
                T value = await _repository.GetValueAsync(entity.Id);

                if (value != null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = new BadRequestResult(),
                        Description = "A value with this name already exists"
                    };
                }

                await _repository.CreateAsync(entity);

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

        public virtual async Task<IBaseResponse<bool>> UpdateAsync(T entity)
        {
            try
            {
                Task<T> value = _repository.GetValueAsync(entity.Id);

                if (value == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = new BadRequestResult(),
                        Description = "A value with this id doesn't exist"
                    };
                }

                await _repository.UpdateAsync(entity);

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
