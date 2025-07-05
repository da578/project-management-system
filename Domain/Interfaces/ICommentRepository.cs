using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        public abstract Task<IEnumerable<Comment>> ReadCommentsByTaskIdAsync(int taskId);
        public abstract Task<IEnumerable<Comment>> ReadCommentsByProjectIdAsync(int projectId);
    }
}