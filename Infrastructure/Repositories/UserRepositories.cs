using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class UserRepository(ProjectManagementDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> ReadByUsernameAsync(string username) => await _dbSet.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<User?> ReadByEmailAsync(string email) => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}