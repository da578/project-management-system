using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class ProjectMemberRepository(ProjectManagementDbContext context) : GenericRepository<ProjectMember>(context), IProjectMemberRepository
    {
        public async Task<IEnumerable<ProjectMember>> ReadMembersByProjectIdAsync(int projectId) =>
            await _dbSet
                .Include(pm => pm.User)
                .Include(pm => pm.Project)
                .Where(pm => pm.ProjectID == projectId)
                .ToListAsync();

        public async Task<IEnumerable<ProjectMember>> ReadProjectsByUserIdAsync(int userId) =>
            await _dbSet
                .Include(pm => pm.User)
                .Include(pm => pm.Project)
                .Where(pm => pm.UserID == userId)
                .ToListAsync();

        public async Task<ProjectMember?> ReadProjectMemberAsync(int projectId, int userId) =>
            await _dbSet
                .Include(pm => pm.User)
                .Include(pm => pm.Project)
                .FirstOrDefaultAsync(pm => pm.ProjectID == projectId && pm.UserID == userId);

        public new async Task<ProjectMember?> ReadByIdAsync(int id) =>
            await _dbSet
                .Include(pm => pm.User)
                .Include(pm => pm.Project)
                .FirstOrDefaultAsync(pm => pm.ProjectMemberID == id);

        public new async Task<IEnumerable<ProjectMember>> ReadAllAsync() =>
            await _dbSet
                .Include(pm => pm.User)
                .Include(pm => pm.Project)
                .ToListAsync();

        Task<IEnumerable<ProjectMember>> IProjectMemberRepository.ReadProjectMemberAsync(int projectId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}