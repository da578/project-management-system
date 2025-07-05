using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        public abstract Task<IEnumerable<Project>> ReadProjectsByUserIdAsync(int userId);
    }
}