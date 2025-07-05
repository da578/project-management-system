using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries
{
    public class ReadCommentByIdQuery(int id) : IRequest<CommentDto>
    {
        public int Id { get; set; } = id;
    }

    public class ReadCommentsByProjectIdQuery(int projectId) : IRequest<IEnumerable<CommentDto>>
    {
        public int ProjectId { get; set; } = projectId;
    }

    public class ReadCommentsByTaskIdQuery(int taskId) : IRequest<IEnumerable<CommentDto>>
    {
        public int TaskId { get; set; } = taskId;
    }
}