using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IProjectMemberRepository : IGenericRepository<ProjectMember>
    {
        public abstract Task<IEnumerable<ProjectMember>> ReadMembersByProjectIdAsync(int projectId);
        public abstract Task<IEnumerable<ProjectMember>> ReadProjectsByUserIdAsync(int userId);
        public abstract Task<IEnumerable<ProjectMember>> ReadProjectMemberAsync(int projectId, int userId);

    }
}