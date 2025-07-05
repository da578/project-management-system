using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Commands
{
    public class CreateProjectMemberCommand(CreateProjectMemberDto projectMemberDto) : IRequest<int>
    {
        public CreateProjectMemberDto ProjectMemberDto { get; set; } = projectMemberDto;
    }

    public class UpdateProjectMemberRoleCommand(int projectMemberId, UpdateProjectMemberRoleDto projectMemberDto) : IRequest<Unit>
    {
        public int ProjectMemberId { get; set; } = projectMemberId;
        public UpdateProjectMemberRoleDto ProjectMemberDto { get; set; } = projectMemberDto;
    }

    public class DeleteProjectMemberCommand(int id) : IRequest<Unit>
    {
        public int ProjectMemberId { get; set; } = id;
    }
}