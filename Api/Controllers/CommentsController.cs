using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var query = new ReadCommentByIdQuery(id);
            var comment = await _mediator.Send(query);
            return comment == null ?
                NotFound($"Comment with ID {id} not found.") :
                Ok(comment);
        }

        [HttpGet("by-task/{taskId}")]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentsByTaskId(int taskId)
        {
            var query = new ReadCommentsByTaskIdQuery(taskId);
            var comments = await _mediator.Send(query);
            return Ok(comments);
        }

        [HttpGet("by-project/{projectId}")]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentsByProjectId(int projectId)
        {
            var query = new ReadCommentsByProjectIdQuery(projectId);
            var comments = await _mediator.Send(query);
            return Ok(comments);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto commentDto)
        {
            var command = new CreateCommentCommand(commentDto);
            try
            {
                var commentId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetCommentById), new { id = commentId }, commentId);
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
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto commentDto)
        {
            var command = new UpdateCommentCommand(id, commentDto);
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
        public async Task<IActionResult> DeleteComment(int id)
        {
            var command = new DeleteCommentCommand(id);
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