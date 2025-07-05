using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ProjectManagementDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ProjectManagementDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task<T?> ReadByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> ReadAllAsync() => await _dbSet.ToListAsync();

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}