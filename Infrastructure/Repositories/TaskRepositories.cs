using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;
using Task = ProjectManagement.Domain.Entities.Task;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class TaskRepository(ProjectManagementDbContext context) : GenericRepository<Task>(context), ITaskRepository
    {
        public async Task<IEnumerable<Task>> ReadTasksByProjectIdAsync(int projectId) =>
            await _dbSet
                .Include(t => t.Project)
                .Include(t => t.AssignedToUser)
                .Where(t => t.ProjectID == projectId)
                .ToListAsync();

        public async Task<IEnumerable<Task>> ReadTasksByAssignedUserIdAsync(int userId) =>
            await _dbSet
                .Include(t => t.Project)
                .Include(t => t.AssignedToUser)
                .Where(t => t.AssignedToUserID == userId)
                .ToListAsync();

        public async Task<Task> ReadByIdAsync(int id) =>
            await _dbSet
                .Include(t => t.Project)
                .Include(t => t.AssignedToUser)
                .FirstOrDefaultAsync(t => t.TaskID == id);

        public async Task<IEnumerable<Task>> ReadAllAsync() =>
            await _dbSet
                .Include(t => t.Project)
                .Include(t => t.AssignedToUser)
                .ToListAsync();
    }
}