using ProjectManagement.Domain.Entities;
using Task = ProjectManagement.Domain.Entities.Task;

namespace ProjectManagement.Domain.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        public abstract Task<IEnumerable<Task>> ReadTasksByProjectIdAsync(int projectId);
        public abstract Task<IEnumerable<Task>> ReadTasksByAssignedUserIdAsync(int userId);
    }
}