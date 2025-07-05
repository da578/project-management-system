using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries.Projects
{
    public class ReadProjectByIdQuery(int id) : IRequest<ProjectDto>
    {
        public int Id { get; set; } = id;
    }
}