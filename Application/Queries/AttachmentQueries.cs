using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries
{
    public class ReadAttachmentByIdQuery(int id) : IRequest<AttachmentDto>
    {
        public int Id { get; set; } = id;
    }

    public class ReadAttachmentsByProjectIdQuery(int projectId) : IRequest<IEnumerable<AttachmentDto>>
    {
        public int ProjectId { get; set; } = projectId;
    }

    public class ReadAttachmentsByTaskIdQuery(int taskId) : IRequest<IEnumerable<AttachmentDto>>
    {
        public int TaskId { get; set; } = taskId;
    }
}