using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Commands
{
    public class CreateProjectCommand(CreateProjectDto projectDto) : IRequest<int>
    {
        public CreateProjectDto ProjectDto { get; set; } = projectDto;
    }

    public class UpdateProjectCommand(int id, UpdateProjectDto projectDto) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
        public UpdateProjectDto ProjectDto = projectDto;
    }

    public class DeleteProjectCommand(int id) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
    }
}