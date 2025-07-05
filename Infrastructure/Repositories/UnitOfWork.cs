using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastucture.Data;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectManagementDbContext _context;

        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public ITaskRepository Tasks { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IAttachmentRepository Attachments { get; private set; }
        public IProjectMemberRepository ProjectMembers { get; private set; }

        public UnitOfWork(ProjectManagementDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Projects = new ProjectRepository(_context);
            Tasks = new TaskRepository(_context);
            Comments = new CommentRepository(_context);
            Attachments = new AttachmentRepository(_context);
            ProjectMembers = new ProjectMemberRepository(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}