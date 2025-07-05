
using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries
{
    public class ReadAllTasksQuery : IRequest<IEnumerable<TaskDto>> { }

    public class ReadTaskByIdQuery(int id) : IRequest<TaskDto>
    {
        public int Id { get; set; } = id;
    }

    public class ReadTasksByAssignedUserIdQuery(int projectId) : IRequest<IEnumerable<TaskDto>>
    {
        public int ProjectId { get; set; } = projectId;
    }

    public class ReadTasksByProjectIdQuery(int projectId) : IRequest<IEnumerable<TaskDto>>
    {
        public int ProjectId { get; set; } = projectId;
    }
}