using AutoMapper;
using MediatR;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Handlers.Projects
{
    public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            _ = _unitOfWork.Users.ReadByIdAsync(request.ProjectDto.UserID) ??
              throw new Exception($"User with ID {request.ProjectDto.UserID} not found.");

            var project = _mapper.Map<Project>(request.ProjectDto);
            project.CreatedAt = DateTime.Now;
            project.UpdatedAt = DateTime.Now;

            await _unitOfWork.Projects.CreateAsync(project);
            await _unitOfWork.CompleteAsync();

            return project.ProjectID;
        }
    }

    public class UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var existingProject = await _unitOfWork.Projects.ReadByIdAsync(request.Id) ??
                throw new Exception($"Project with ID {request.Id} not found.");

            _mapper.Map(request.ProjectDto, existingProject);
            existingProject.UpdatedAt = DateTime.Now;

            _unitOfWork.Projects.Update(existingProject);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class DeleteProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToDelete = await _unitOfWork.Projects.ReadByIdAsync(request.Id) ??
                throw new Exception($"Project with ID {request.Id} not found.");

            _unitOfWork.Projects.Delete(projectToDelete);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class ReadAllProjectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProjectDto>> Handle(ReadAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.Projects.ReadAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }
    }

    public class ReadProjectByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadProjectByIdQuery, ProjectDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ProjectDto> Handle(ReadProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.ReadByIdAsync(request.Id) ??
                throw new Exception($"Project with ID {request.Id} not found.");
            return _mapper.Map<ProjectDto>(project);
        }
    }
}
