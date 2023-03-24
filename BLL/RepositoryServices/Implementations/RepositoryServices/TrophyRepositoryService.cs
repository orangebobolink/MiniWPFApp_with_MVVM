using AutoMapper;
using BLL.DTO;
using BLL.RepositoryServices.Interfaces.RepositoryServices;
using DAL.Domain.Interfaces.ImplementationsOfRepository;
using DAL.Domain.Interfaces;
using DAL.Domain.Models;
using DAL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BLL.RepositoryServices.Implementations.RepositoryServices
{
    public class TrophyRepositoryService : ITrophyRepositoryService
    {
        private ITrophyRepository _repository;
        private IMapper _mapper;

        public TrophyRepositoryService(ITrophyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(TrophyDTO entity)
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

                var trophy = _mapper.Map<Trophy>(entity);
                await _repository.DeleteAsync(trophy);

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

        public async Task<IBaseResponse<List<TrophyDTO>>> GetAllAsync()
        {
            try
            {
                var values = _mapper.Map<List<TrophyDTO>>(await _repository.GetAllAsync());

                return new BaseResponse<List<TrophyDTO>>()
                {
                    Data = values,
                    Description = "Data received successfully.",
                    StatusCode = new OkResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<TrophyDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<TrophyDTO>> GetValueAsync(int id)
        {
            try
            {
                var entity = _mapper.Map<TrophyDTO>(await _repository.GetValueAsync(id));

                if (entity == null)
                {
                    return new BaseResponse<TrophyDTO>()
                    {
                        StatusCode = new BadRequestResult(),
                        Description = "A value with this id doesn't exist"
                    };
                }

                return new BaseResponse<TrophyDTO>()
                {
                    Data = entity,
                    Description = "The value was successfully found.",
                    StatusCode = new BadRequestResult()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TrophyDTO>()
                {
                    Description = ex.Message,
                    StatusCode = new BadRequestResult()
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateAsync(TrophyDTO entity)
        {
            try
            {
                var trophy = _mapper.Map<Trophy>(entity);
                await _repository.CreateAsync(trophy);

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

        public async Task<IBaseResponse<bool>> UpdateAsync(TrophyDTO entity)
        {
            try
            {
                var trophy = _mapper.Map<Trophy>(entity);
                await _repository.UpdateAsync(trophy);

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
