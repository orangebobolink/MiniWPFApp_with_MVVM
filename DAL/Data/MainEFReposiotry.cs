using DAL.Domain;
using DAL.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    internal abstract class MainEFReposiotry<T>
        : IRepository<T> where T : EntityBase
    {
        private readonly ApplicationContext _dbContext;

        public MainEFReposiotry(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetValueAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
