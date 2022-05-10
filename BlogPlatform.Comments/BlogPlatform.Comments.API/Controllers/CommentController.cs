using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using BlogPlatform.Comments.BusinessLogic.DTO.Responses;
using BlogPlatform.Comments.BusinessLogic.Helpers;
using BlogPlatform.Comments.BusinessLogic.Services.Contracts;
using BlogPlatform.Comments.DataAccess.Extensions;
using BlogPlatform.Comments.DataAccess.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Comments.API.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("post/{postId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Page<CommentResponse>>> GetPageOfCommentsForPost(
        [FromRoute] Guid postId, [FromQuery] CommentFilter filter)
    {
        return await _commentService.GetPageOfCommentsForPostAsync(postId, filter);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentResponse>> GetCommentById([FromRoute] Guid id)
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
    public async Task<ActionResult<CommentResponse>> PublishComment([FromBody] CommentRequest commentDto)
    {
        string userId = HttpContext.User?.FindFirst("id")?.Value;
        return await _commentService.PublishCommentAsync(commentDto, userId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> EditComment(
        [FromRoute] Guid id, [FromBody] CommentContentRequest commentDto)
    {
        try
        {
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteComment([FromRoute] Guid id)
    {
        try
        {
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PatchCommentVotes(
        [FromRoute] Guid id, [FromBody] CommentVoteRequest voteRequest)
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

    private async Task<bool> CheckIsAuthorOfCommentOrAdmin(Guid id)
    {
        string userId = HttpContext.User.FindFirst("id").Value;
        return await _commentService.CheckIsCommentAuthorAsync(id, userId)
               || HttpContext.User.IsInRole("Admin");
    }
}
