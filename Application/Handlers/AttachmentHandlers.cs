using AutoMapper;
using MediatR;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;
using ProjectManagement.Domain.Interfaces;
using Attachment = ProjectManagement.Domain.Entities.Attachment;

namespace ProjectManagement.Application.Handlers
{
    public class CreateAttachmentCommandHandlers(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateAttachmentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            if ((request.AttachmentDto.TaskID.HasValue && request.AttachmentDto.ProjectID.HasValue) ||
                (!request.AttachmentDto.TaskID.HasValue && !request.AttachmentDto.ProjectID.HasValue))
                throw new Exception("An attachment must be associated with either a Task or a Project, but not both.");

            _ = await _unitOfWork.Users.ReadByIdAsync(request.AttachmentDto.UserID) ??
                throw new Exception($"User with ID {request.AttachmentDto.UserID} not found.");

            if (request.AttachmentDto.TaskID.HasValue)
                _ = await _unitOfWork.Tasks.ReadByIdAsync(request.AttachmentDto.TaskID.Value) ??
                    throw new Exception($"Task with ID {request.AttachmentDto.TaskID.Value} not found.");

            if (request.AttachmentDto.ProjectID.HasValue)
                _ = await _unitOfWork.Projects.ReadByIdAsync(request.AttachmentDto.ProjectID.Value) ??
                    throw new Exception($"Project with ID {request.AttachmentDto.ProjectID.Value} not found.");

            var attachment = _mapper.Map<Attachment>(request.AttachmentDto);
            attachment.UploadedAt = DateTime.Now;

            await _unitOfWork.Attachments.CreateAsync(attachment);
            await _unitOfWork.CompleteAsync();

            return attachment.AttachmentID;
        }
    }

    public class UpdateAttachmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateAttachmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(UpdateAttachmentCommand request, CancellationToken cancellationToken)
        {
            var existingAttachment = await _unitOfWork.Attachments.ReadByIdAsync(request.Id) ??
                throw new Exception($"Attachment with ID {request.Id} not found.");

            if (existingAttachment.UserID != request.AttachmentDto.UserID)
                throw new UnauthorizedAccessException("You are not authorized to update this attachment.");

            _mapper.Map(request.AttachmentDto, existingAttachment);

            _unitOfWork.Attachments.Update(existingAttachment);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class DeleteAttachmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteAttachmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            var attachmentToDelete = await _unitOfWork.Attachments.ReadByIdAsync(request.Id) ??
                throw new Exception($"Attachment with ID {request.Id} not found.");

            _unitOfWork.Attachments.Delete(attachmentToDelete);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class GetAttachmentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadAttachmentByIdQuery, AttachmentDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<AttachmentDto> Handle(ReadAttachmentByIdQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _unitOfWork.Attachments.ReadByIdAsync(request.Id) ??
                throw new Exception($"Attachment with ID {request.Id} not found.");
            return _mapper.Map<AttachmentDto>(attachment);
        }
    }

    public class GetAttachmentsByTaskIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadAttachmentsByTaskIdQuery, IEnumerable<AttachmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<AttachmentDto>> Handle(ReadAttachmentsByTaskIdQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _unitOfWork.Attachments.GetAttachmentsByTaskIdAsync(request.TaskId);
            return _mapper.Map<IEnumerable<AttachmentDto>>(attachments);
        }
    }

    public class GetAttachmentsByProjectIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadAttachmentsByProjectIdQuery, IEnumerable<AttachmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<AttachmentDto>> Handle(ReadAttachmentsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _unitOfWork.Attachments.GetAttachmentsByProjectIdAsync(request.ProjectId);
            return _mapper.Map<IEnumerable<AttachmentDto>>(attachments);
        }
    }
}