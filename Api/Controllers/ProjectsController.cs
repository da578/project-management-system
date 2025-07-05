using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects()
        {
            var query = new ReadAllProjectsQuery();
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var query = new ReadProjectsByUserIdQuery(id);
            var project = await _mediator.Send(query);

            return project == null ?
                NotFound($"Project with ID {id} not found.") :
                Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto projectDto)
        {
            var command = new CreateProjectCommand(projectDto);
            try
            {
                var projectId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, projectId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
        {
            var command = new UpdateProjectCommand(id, projectDto);
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var command = new DeleteProjectCommand(id);
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}