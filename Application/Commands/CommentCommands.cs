using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Commands
{
    public class CreateCommentCommand(CreateCommentDto commentDto) : IRequest<int>
    {
        public CreateCommentDto CommentDto { get; set; } = commentDto;
    }

    public class UpdateCommentCommand(int id, UpdateCommentDto commentDto) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
        public UpdateCommentDto CommentDto { get; set; } = commentDto;
    }

    public class DeleteCommentCommand(int id) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
    }
}