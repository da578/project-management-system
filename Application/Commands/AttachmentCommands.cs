using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Commands
{
    public class CreateAttachmentCommand(CreateAttachmentDto attachmentDto) : IRequest<int>
    {
        public CreateAttachmentDto AttachmentDto { get; set; } = attachmentDto;
    }

    public class UpdateAttachmentCommand(int id, UpdateAttachmentDto attachmentDto) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
        public UpdateAttachmentDto AttachmentDto { get; set; } = attachmentDto;
    }

    public class DeleteAttachmentCommand(int id) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
    }
}