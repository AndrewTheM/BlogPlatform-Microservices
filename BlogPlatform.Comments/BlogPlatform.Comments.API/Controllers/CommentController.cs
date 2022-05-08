using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using BlogPlatform.Comments.BusinessLogic.DTO.Responses;
using BlogPlatform.Comments.BusinessLogic.Services.Contracts;
using BlogPlatform.Comments.DataAccess.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPlatform.Comments.API.Controllers
{
    [Route("api/comments")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentResponse>> GetCommentById([FromRoute] int id)
        {
            try
            {
                return await _commentService.GetCommentByIdAsync(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CommentResponse>> CreateComment([FromBody] CommentRequest commentDto)
        {
            string userId = HttpContext.User.FindFirst("id").Value;
            return await _commentService.PublishCommentAsync(commentDto, userId);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateComment([FromRoute] int id,
                                                      [FromBody] CommentRequest commentDto)
        {
            try
            {
                bool userIsPermitted = await CheckIsAuthorOfCommentOrAdmin(id);

                if (!userIsPermitted)
                    return Forbid();

                await _commentService.EditCommentAsync(id, commentDto);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteComment([FromRoute] int id)
        {
            try
            {
                bool userIsPermitted = await CheckIsAuthorOfCommentOrAdmin(id);

                if (!userIsPermitted)
                    return Forbid();

                await _commentService.DeleteCommentAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PatchCommentVotes([FromRoute] int id,
                                                          [FromBody] CommentVoteRequest voteRequest)
        {
            try
            {
                await _commentService.AddVoteToCommentAsync(id, voteRequest.VoteValue);
                return NoContent();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        private async Task<bool> CheckIsAuthorOfCommentOrAdmin(int id)
        {
            string userId = HttpContext.User.FindFirst("id").Value;
            return await _commentService.CheckIsCommentAuthorAsync(id, userId)
                   || HttpContext.User.IsInRole("Admin");
        }
    }
}
