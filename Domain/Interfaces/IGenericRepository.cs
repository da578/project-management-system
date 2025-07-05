namespace ProjectManagement.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public abstract Task CreateAsync(T entity);
        public abstract Task<T?> ReadByIdAsync(int id);
        public abstract Task<IEnumerable<T>> ReadAllAsync();
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
    }
}