using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AttachmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAttachmentById(int id)
        {
            var query = new ReadAttachmentByIdQuery(id);
            var attachment = await _mediator.Send(query);
            return attachment == null ?
                NotFound($"Attachment with ID {id} not found.") :
                Ok(attachment);
        }

        [HttpGet("by-task/{taskId}")]
        [ProducesResponseType(typeof(IEnumerable<AttachmentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAttachmentsByTaskId(int taskId)
        {
            var query = new ReadAttachmentsByTaskIdQuery(taskId);
            var attachments = await _mediator.Send(query);
            return Ok(attachments);
        }

        [HttpGet("by-project/{projectId}")]
        [ProducesResponseType(typeof(IEnumerable<AttachmentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAttachmentsByProjectId(int projectId)
        {
            var query = new ReadAttachmentsByProjectIdQuery(projectId);
            var attachments = await _mediator.Send(query);
            return Ok(attachments);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAttachment([FromBody] CreateAttachmentDto attachmentDto)
        {
            var command = new CreateAttachmentCommand(attachmentDto);
            try
            {
                var attachmentId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAttachmentById), new { id = attachmentId }, attachmentId);
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
        public async Task<IActionResult> UpdateAttachment(int id, [FromBody] UpdateAttachmentDto attachmentDto)
        {
            var command = new UpdateAttachmentCommand(id, attachmentDto);
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
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var command = new DeleteAttachmentCommand(id);
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