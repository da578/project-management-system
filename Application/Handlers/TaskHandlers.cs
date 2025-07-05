using AutoMapper;
using MediatR;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Handlers
{
    public class CreateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            _ = await _unitOfWork.Tasks.ReadByIdAsync(request.TaskDto.ProjectId) ??
                throw new Exception($"Project with ID {request.TaskDto.ProjectId} not found.");

            if (request.TaskDto.AssignedToUserId.HasValue)
            {
                _ = await _unitOfWork.Users.ReadByIdAsync(request.TaskDto.AssignedToUserId.Value) ??
                throw new Exception($"Assigned to user with ID {request.TaskDto.AssignedToUserId.Value} not found.");
            }

            var task = _mapper.Map<ProjectManagement.Domain.Entities.Task>(request.TaskDto);
            task.CreatedAt = DateTime.Now;
            task.UpdatedAt = DateTime.Now;

            await _unitOfWork.Tasks.CreateAsync(task);
            await _unitOfWork.CompleteAsync();

            return task.TaskID;
        }

        public class UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateTaskCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
            {
                var existingTask = await _unitOfWork.Tasks.ReadByIdAsync(request.Id) ??
                    throw new Exception($"Task with ID {request.Id} not found.");

                if (request.TaskDto.AssignedToUserId.HasValue && request.TaskDto.AssignedToUserId != existingTask.AssignedToUserID)
                    _ = await _unitOfWork.Users.ReadByIdAsync(request.TaskDto.AssignedToUserId.Value) ??
                        throw new Exception($"Assigned user with ID {request.TaskDto.AssignedToUserId.Value} not found.");

                _mapper.Map(request.TaskDto, existingTask);
                existingTask.UpdatedAt = DateTime.Now;

                _unitOfWork.Tasks.Update(existingTask);
                await _unitOfWork.CompleteAsync();

                return Unit.Value;
            }
        }

        public class DeleteTaskCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTaskCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;

            public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                var taskToDelete = await _unitOfWork.Tasks.ReadByIdAsync(request.Id) ??
                    throw new Exception($"Task with ID {request.Id} not found.");

                _unitOfWork.Tasks.Delete(taskToDelete);
                await _unitOfWork.CompleteAsync();

                return Unit.Value;
            }
        }

        public class AssignTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AssignTaskCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<Unit> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
            {
                var taskToAssign = await _unitOfWork.Tasks.ReadByIdAsync(request.TaskId) ??
                    throw new Exception($"Task with ID {request.TaskId} not found.");

                if (request.AssignedToUserId.HasValue)
                    _ = await _unitOfWork.Users.ReadByIdAsync(request.AssignedToUserId.Value) ??
                        throw new Exception($"User with ID {request.AssignedToUserId.Value} not found.");

                taskToAssign.AssignedToUserID = request.AssignedToUserId;
                taskToAssign.UpdatedAt = DateTime.Now;

                _unitOfWork.Tasks.Update(taskToAssign);
                await _unitOfWork.CompleteAsync();

                return Unit.Value;
            }
        }

        public class ReadTaskByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadTaskByIdQuery, TaskDto>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<TaskDto> Handle(ReadTaskByIdQuery request, CancellationToken cancellationToken)
            {
                var task = await _unitOfWork.Tasks.ReadByIdAsync(request.Id) ??
                    throw new Exception($"Task with ID {request.Id} not found.");
                return _mapper.Map<TaskDto>(task);
            }
        }

        public class ReadAllTasksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadAllTasksQuery, IEnumerable<TaskDto>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<IEnumerable<TaskDto>> Handle(ReadAllTasksQuery request, CancellationToken cancellationToken)
            {
                var tasks = await _unitOfWork.Tasks.ReadAllAsync();
                return _mapper.Map<IEnumerable<TaskDto>>(tasks);
            }
        }

        public class ReadTasksByProjectIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadTasksByProjectIdQuery, IEnumerable<TaskDto>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<IEnumerable<TaskDto>> Handle(ReadTasksByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var tasks = await _unitOfWork.Tasks.ReadTasksByProjectIdAsync(request.ProjectId);
                return _mapper.Map<IEnumerable<TaskDto>>(tasks);
            }
        }

        
        public class ReadTasksByAssignedUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadTasksByAssignedUserIdQuery, IEnumerable<TaskDto>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<IEnumerable<TaskDto>> Handle(ReadTasksByAssignedUserIdQuery request, CancellationToken cancellationToken)
            {
                var tasks = await _unitOfWork.Tasks.ReadTasksByAssignedUserIdAsync(request.ProjectId);
                return _mapper.Map<IEnumerable<TaskDto>>(tasks);
            }
        }
    }
}