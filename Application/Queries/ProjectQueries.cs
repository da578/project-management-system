
using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries
{
    public class ReadAllProjectsQuery : IRequest<IEnumerable<ProjectDto>> { }

    public class ReadProjectByIdQuery(int id) : IRequest<ProjectDto>
    {
        public int Id { get; set; } = id;
    }
}