using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IAttachmentRepository : IGenericRepository<Attachment>
    {
        public abstract Task<IEnumerable<Attachment>> GetAttachmentsByTaskIdAsync(int taskId);
        public abstract Task<IEnumerable<Attachment>> GetAttachmentsByProjectIdAsync(int projectId);
    }
}