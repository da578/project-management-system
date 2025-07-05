using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ProjectManagementDbContext context) : base(context) { }

        public async Task<IEnumerable<Attachment>> GetAttachmentsByTaskIdAsync(int taskId) =>
            await _dbSet
                .Include(a => a.User)
                .Where(a => a.TaskID == taskId)
                .ToListAsync();

        public async Task<IEnumerable<Attachment>> GetAttachmentsByProjectIdAsync(int projectId) =>
            await _dbSet
                .Include(a => a.User)
                .Where(a => a.ProjectID == projectId)
                .ToListAsync();
        
        public new async Task<Attachment?> ReadByIdAsync(int id) =>
            await _dbSet
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AttachmentID == id);

        public new async Task<IEnumerable<Attachment>> ReadAllAsync() =>
            await _dbSet
                .Include(a => a.User)
                .ToListAsync();
    }
}