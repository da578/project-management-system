using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;
namespace ProjectManagement.Infrastructure.Repositories
{
    public class CommentRepository(ProjectManagementDbContext context) : GenericRepository<Comment>(context), ICommentRepository
    {
        public async Task<IEnumerable<Comment>> ReadCommentsByTaskIdAsync(int taskId) =>
             await _dbSet
                .Include(c => c.User)
                .Where(c => c.TaskID == taskId)
                .ToListAsync();

        public async Task<IEnumerable<Comment>> ReadCommentsByProjectIdAsync(int projectId) =>
             await _dbSet
                .Include(c => c.User)
                .Where(c => c.ProjectID == projectId)
                .ToListAsync();

        public new async Task<Comment?> ReadByIdAsync(int id) =>
             await _dbSet
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CommentID == id);

        public new async Task<IEnumerable<Comment>> ReadAllAsync() =>
             await _dbSet
                .Include(c => c.User)
                .ToListAsync();
    }
}