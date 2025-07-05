using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries
{
    public class ReadProjectMemberByIdQuery(int id) : IRequest<ProjectMemberDto>
    {
        public int ProjectMemberId { get; set; } = id;
    }

    public class ReadProjectMemberByProjectAndUserQuery(int projectId, int userId) : IRequest<ProjectMemberDto>
    {
        public int ProjectId { get; set; } = projectId;
        public int UserId { get; set; } = userId;
    }

    public class ReadProjectMembersByProjectIdQuery(int projectId) : IRequest<IEnumerable<ProjectMemberDto>>
    {
        public int ProjectId { get; set; } = projectId;
    }

    public class ReadProjectsByUserIdQuery(int userId) : IRequest<IEnumerable<ProjectMemberDto>>
    {
        public int UserId { get; set; } = userId;
    }
}