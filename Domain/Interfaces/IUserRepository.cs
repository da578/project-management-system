using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public abstract Task<User?> ReadByUsernameAsync(string username);
        public abstract Task<User?> ReadByEmailAsync(string email);
    }
}