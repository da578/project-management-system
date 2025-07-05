using ProjectManagement.Application.Mappers;
using ProjectManagement.Application.Queries;

namespace ProjectManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ReadAllProjectsQuery).Assembly));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            return services;
        }
    }
}