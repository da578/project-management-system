using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using Task = ProjectManagement.Domain.Entities.Task;

namespace ProjectManagement.Infrastucture.Data
{
    public class ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectMember>()
                .HasIndex(projectMember => new { projectMember.ProjectID, projectMember.UserID })
                .IsUnique();

            modelBuilder.Entity<ProjectMember>()
                .HasOne(projectMember => projectMember.Project)
                .WithMany(project => project.ProjectMembers)
                .HasForeignKey(projectMember => projectMember.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMemberships)
                .HasForeignKey(pm => pm.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(project => project.CreatedByUser)
                .WithMany(user => user.CreatedProjects)
                .HasForeignKey(project => project.CreatedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.Task)
                .WithMany(task => task.Comments)
                .HasForeignKey(comment => comment.TaskID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.Project)
                .WithMany(project => project.Comments)
                .HasForeignKey(comment => comment.ProjectID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attachment>()
                .HasOne(attachment => attachment.Task)
                .WithMany(task => task.Attachments)
                .HasForeignKey(attachment => attachment.TaskID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attachment>()
                .HasOne(attachment => attachment.Project)
                .WithMany(project => project.Attachments)
                .HasForeignKey(attachment => attachment.ProjectID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attachment>()
                .HasOne(attachment => attachment.User)
                .WithMany(user => user.Attachments)
                .HasForeignKey(attachment => attachment.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}