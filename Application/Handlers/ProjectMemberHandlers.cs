using AutoMapper;
using MediatR;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Handlers
{
    public class CreateProjectMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateProjectMemberCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateProjectMemberCommand request, CancellationToken cancellationToken)
        {
            _ = await _unitOfWork.Projects.ReadProjectsByUserIdAsync(request.ProjectMemberDto.ProjectID) ??
          throw new Exception($"Project with ID {request.ProjectMemberDto.ProjectID} not found.");

            _ = await _unitOfWork.Users.ReadByIdAsync(request.ProjectMemberDto.UserID) ??
           throw new Exception($"User with ID {request.ProjectMemberDto.UserID} not found.");

            var existingMember = await _unitOfWork.ProjectMembers.ReadProjectMemberAsync(request.ProjectMemberDto.ProjectID, request.ProjectMemberDto.UserID);
            if (existingMember != null)
                throw new Exception($"User with ID {request.ProjectMemberDto.UserID} is already a member of project {request.ProjectMemberDto.ProjectID}.");

            var projectMember = _mapper.Map<ProjectMember>(request.ProjectMemberDto);
            projectMember.JoinedAt = DateTime.Now;

            await _unitOfWork.ProjectMembers.CreateAsync(projectMember);
            await _unitOfWork.CompleteAsync();
            return projectMember.ProjectMemberID;
        }

        public class UpdateProjectMemberRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateProjectMemberRoleCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<Unit> Handle(UpdateProjectMemberRoleCommand request, CancellationToken cancellationToken)
            {
                var existingProjectMember = await _unitOfWork.ProjectMembers.ReadByIdAsync(request.ProjectMemberId) ??
                throw new Exception($"Project member with ID {request.ProjectMemberId} not found.");

                _mapper.Map(request.ProjectMemberDto, existingProjectMember);
                _unitOfWork.ProjectMembers.Update(existingProjectMember);
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }
        }

        public class DeleteProjectMemberCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectMemberCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;

            public async Task<Unit> Handle(DeleteProjectMemberCommand request, CancellationToken cancellationToken)
            {
                var projectMemberToDelete = await _unitOfWork.ProjectMembers.ReadByIdAsync(request.ProjectMemberId) ??
                throw new Exception($"Project member with ID {request.ProjectMemberId} not found.");

                _unitOfWork.ProjectMembers.Delete(projectMemberToDelete);
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }
        }

        public class GetProjectMemberByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadProjectMemberByIdQuery, ProjectMemberDto>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<ProjectMemberDto> Handle(ReadProjectMemberByIdQuery request, CancellationToken cancellationToken)
            {
                var projectMember = await _unitOfWork.ProjectMembers.ReadByIdAsync(request.ProjectMemberId) ??
                throw new Exception($"Project member with ID {request.ProjectMemberId} not found.");
                return _mapper.Map<ProjectMemberDto>(projectMember);
            }
        }

        public class ReadProjectMembersByProjectIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadProjectMembersByProjectIdQuery, IEnumerable<ProjectMemberDto>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<IEnumerable<ProjectMemberDto>> Handle(ReadProjectMembersByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var projectMembers = await _unitOfWork.ProjectMembers.ReadMembersByProjectIdAsync(request.ProjectId) ??
                throw new Exception($"Project with ID {request.ProjectId} not found.");
                return _mapper.Map<IEnumerable<ProjectMemberDto>>(projectMembers);
            }
        }

        public class ReadProjectsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadProjectsByUserIdQuery, IEnumerable<ProjectMemberDto>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<IEnumerable<ProjectMemberDto>> Handle(ReadProjectsByUserIdQuery request, CancellationToken cancellationToken)
            {
                var projectMemberships = await _unitOfWork.ProjectMembers.ReadProjectsByUserIdAsync(request.UserId);
                return _mapper.Map<IEnumerable<ProjectMemberDto>>(projectMemberships);
            }
        }

        public class ReadProjectMemberByProjectAndUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadProjectMemberByProjectAndUserQuery, ProjectMemberDto>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<ProjectMemberDto> Handle(ReadProjectMemberByProjectAndUserQuery request, CancellationToken cancellationToken)
            {
                var projectMember = await _unitOfWork.ProjectMembers.ReadProjectMemberAsync(request.ProjectId, request.UserId) ??
                    throw new Exception($"Project member with project ID {request.ProjectId} and user ID {request.UserId} not found.");
                return _mapper.Map<ProjectMemberDto>(projectMember);
            }
        }
    }
}