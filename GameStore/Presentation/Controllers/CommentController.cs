using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForUpdate;

namespace Presentation.Controllers
{
    [Route("api/comments")]
    [Authorize]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CommentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("game/{id:guid}", Name = "CommentByGameId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments(Guid id)
        {
            var comments = await _service.CommentService.GetAllCommentsAsync(id,false);

            return Ok(comments);
        }


        [HttpGet("{id:guid}", Name = "CommentById")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var comment = await _service.CommentService.GetCommentAsync(id, false);
            return Ok(comment);
        }

        

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CommentForCreationDto comment)
        {
            if (comment.Body?.Length>600)
            {
                return BadRequest("Comment is to big. The size of a comment should be 600 characters. ");
            }
            
            
            var createdComment = await _service.CommentService.CreateCommentAsync(comment, User.Identity.Name);

            return CreatedAtRoute("CommentById", new { id = createdComment.Id }, createdComment);
        }

       

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _service.CommentService.DeleteCommentAsync(id, false);

            return NoContent();
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> UpdateComment( [FromBody] CommentForUpdateDto comment)
        {
            await _service.CommentService.UpdateCommentAsync( comment, true);

            return NoContent();
        }
    }
}