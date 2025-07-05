using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class ProjectRepository(ProjectManagementDbContext context) : GenericRepository<Project>(context), IProjectRepository
    {
        public async Task<IEnumerable<Project>> ReadProjectsByUserIdAsync(int userId) =>
            await _dbSet
                .Include(p => p.CreatedByUser)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .Where(p => p.CreatedByUserID == userId || (p.ProjectMembers != null && p.ProjectMembers.Any(pm => pm.UserID == userId)))
                .ToListAsync();

        public new async Task<Project?> ReadByIdAsync(int id) =>
            await _dbSet
                .Include(p => p.CreatedByUser)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .FirstOrDefaultAsync(p => p.ProjectID == id);

        public new async Task<IEnumerable<Project>> ReadAllAsync() =>
            await _dbSet
                .Include(p => p.CreatedByUser)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .ToListAsync();
    }
}