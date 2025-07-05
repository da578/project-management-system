using AutoMapper;
using MediatR;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Handlers
{
    public class CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if ((request.CommentDto.TaskID.HasValue && request.CommentDto.ProjectID.HasValue) || (!request.CommentDto.TaskID.HasValue && !request.CommentDto.ProjectID.HasValue))
                throw new Exception("A comment must be associated with either a Task or a Project, but not both.");

            _ = await _unitOfWork.Users.ReadByIdAsync(request.CommentDto.UserID) ??
            throw new Exception($"User with ID {request.CommentDto.UserID} not found.");

            if (request.CommentDto.TaskID.HasValue)
                _ = await _unitOfWork.Tasks.ReadByIdAsync(request.CommentDto.TaskID.Value) ??
                    throw new Exception($"Task with ID {request.CommentDto.TaskID.Value} not found.");

            if (request.CommentDto.ProjectID.HasValue)
                _ = await _unitOfWork.Projects.ReadByIdAsync(request.CommentDto.ProjectID.Value) ??
                    throw new Exception($"Project with ID {request.CommentDto.ProjectID.Value} not found.");

            var comment = _mapper.Map<Comment>(request.CommentDto);
            comment.CreatedAt = DateTime.Now;

            await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.CompleteAsync();

            return comment.CommentID;
        }
    }

    public class UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var existingComment = await _unitOfWork.Comments.ReadByIdAsync(request.Id) ??
                throw new Exception($"Comment with ID {request.Id} not found.");

            if (existingComment.UserID != request.CommentDto.UserID)
                throw new UnauthorizedAccessException("You are not authorized to update this comment.");

            _mapper.Map(request.CommentDto, existingComment);

            _unitOfWork.Comments.Update(existingComment);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class DeleteCommentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToDelete = await _unitOfWork.Comments.ReadByIdAsync(request.Id) ??
                throw new Exception($"Comment with ID {request.Id} not found.");

            _unitOfWork.Comments.Delete(commentToDelete);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class ReadCommentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadCommentByIdQuery, CommentDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CommentDto> Handle(ReadCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.Comments.ReadByIdAsync(request.Id) ??
                throw new Exception($"Comment with ID {request.Id} not found.");
            return _mapper.Map<CommentDto>(comment);
        }
    }

    public class ReadCommentsByTaskIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadCommentsByTaskIdQuery, IEnumerable<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CommentDto>> Handle(ReadCommentsByTaskIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _unitOfWork.Comments.ReadCommentsByTaskIdAsync(request.TaskId);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }
    }

    public class ReadCommentsByProjectIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadCommentsByProjectIdQuery, IEnumerable<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork; 
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CommentDto>> Handle(ReadCommentsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _unitOfWork.Comments.ReadCommentsByProjectIdAsync(request.ProjectId);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }
    }
}