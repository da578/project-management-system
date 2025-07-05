using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTasks()
        {
            var query = new ReadAllTasksQuery();
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var query = new ReadTaskByIdQuery(id);
            var task = await _mediator.Send(query);
            return task == null ?
                NotFound($"Task with ID {id} not found.") :
                Ok(task);
        }

        [HttpGet("by-project/{projectId}")]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var query = new ReadTasksByProjectIdQuery(projectId);
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        [HttpGet("by-assigned-user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByAssignedUser(int userId)
        {
            var query = new ReadTasksByAssignedUserIdQuery(userId);
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto taskDto)
        {
            var command = new CreateTaskCommand(taskDto);
            try
            {
                var taskId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetTaskById), new { id = taskId }, taskId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto taskDto)
        {
            var command = new UpdateTaskCommand(id, taskDto);
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

        [HttpPatch("{id}/assign")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignTask(int id, [FromQuery] int? userId)
        {
            var command = new AssignTaskCommand(id, userId);
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
        public async Task<IActionResult> DeleteTask(int id)
        {
            var command = new DeleteTaskCommand(id);
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