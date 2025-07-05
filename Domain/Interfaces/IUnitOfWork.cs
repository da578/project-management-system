namespace ProjectManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public abstract IUserRepository Users { get; }
        public abstract IProjectRepository Projects { get; }
        public abstract ITaskRepository Tasks { get; }
        public abstract ICommentRepository Comments { get; }
        public abstract IAttachmentRepository Attachments { get; }
        public abstract IProjectMemberRepository ProjectMembers { get; }
        public abstract Task<int> CompleteAsync();
    }
}