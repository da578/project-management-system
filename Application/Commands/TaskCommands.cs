using MediatR;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Commands
{
    public class CreateTaskCommand(CreateTaskDto taskDto) : IRequest<int>
    {
        public CreateTaskDto TaskDto { get; set; } = taskDto;
    }

    public class AssignTaskCommand(int taskId, int? assignedToUserId) : IRequest<Unit>
    {
        public int TaskId { get; set; } = taskId;
        public int? AssignedToUserId { get; set; } = assignedToUserId;
    }

    public class UpdateTaskCommand(int id, UpdateTaskDto taskDto) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
        public UpdateTaskDto TaskDto { get; set; } = taskDto;
    }

    public class DeleteTaskCommand(int id) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
    }
}