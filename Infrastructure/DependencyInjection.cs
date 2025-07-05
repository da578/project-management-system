using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastructure.Repositories;
using ProjectManagement.Infrastucture.Data;

namespace ProjectManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();

            return services;
        }
    }
}
