using DAL.Domain.Interfaces;

namespace BLL.RepositoryServices.Interfaces
{
    public interface IRepositoryService<T>
    {
        public Task<IBaseResponse<T>> GetValueAsync(int id);
        public Task<IBaseResponse<List<T>>> GetAllAsync();
        public Task<IBaseResponse<bool>> DeleteAsync(T entity);
        public Task<IBaseResponse<bool>> UpdateAsync(T entity);
        public Task<IBaseResponse<bool>> CreateAsync(T entity);
        public Task Save();
    }
}
