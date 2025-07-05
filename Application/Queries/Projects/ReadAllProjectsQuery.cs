using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries.Projects
{
    public class ReadAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
    {
    }
}