using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectMembersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectMemberDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectMemberById(int id)
        {
            var query = new ReadProjectMemberByIdQuery(id);
            var projectMember = await _mediator.Send(query);
            return projectMember == null ?
                NotFound($"Project member with ID {id} not found.") :
                Ok(projectMember);
        }

        [HttpGet("by-project/{projectId}")]
        [ProducesResponseType(typeof(IEnumerable<ProjectMemberDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectMembersByProjectId(int projectId)
        {
            var query = new ReadProjectMembersByProjectIdQuery(projectId);
            var projectMembers = await _mediator.Send(query);
            return Ok(projectMembers);
        }

        [HttpGet("by-user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<ProjectMemberDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectsByUserId(int userId)
        {
            var query = new ReadProjectsByUserIdQuery(userId);
            var projectMemberships = await _mediator.Send(query);
            return Ok(projectMemberships);
        }

        [HttpGet("by-project-user")]
        [ProducesResponseType(typeof(ProjectMemberDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectMemberByProjectAndUser([FromQuery] int projectId, [FromQuery] int userId)
        {
            var query = new ReadProjectMemberByProjectAndUserQuery(projectId, userId);
            var projectMember = await _mediator.Send(query);
            return projectMember == null ?
                NotFound($"Project member for Project ID {projectId} and User ID {userId} not found.") :
                Ok(projectMember);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProjectMember([FromBody] CreateProjectMemberDto projectMemberDto)
        {
            var command = new CreateProjectMemberCommand(projectMemberDto);
            try
            {
                var projectMemberId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetProjectMemberById), new { id = projectMemberId }, projectMemberId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProjectMemberRole(int id, [FromBody] UpdateProjectMemberRoleDto roleDto)
        {
            var command = new UpdateProjectMemberRoleCommand(id, roleDto);
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ex.Message.Contains("not found") ?
                    NotFound(ex.Message) :
                    BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveProjectMember(int id)
        {
            var command = new DeleteProjectMemberCommand(id);
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ex.Message.Contains("not found") ?
                    NotFound(ex.Message) :
                    BadRequest(ex.Message);
            }
        }
    }
}