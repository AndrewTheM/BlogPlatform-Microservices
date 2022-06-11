using BlogPlatform.Shared.Common.Filters;
using BlogPlatform.Shared.Common.Pagination;
using Comments.BusinessLogic.DTO.Requests;
using Comments.BusinessLogic.DTO.Responses;
using Comments.BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Comments.API.Controllers;

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
        var page = await _commentService.GetPageOfCommentsForPostAsync(postId, filter);
        return Ok(page);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentResponse>> GetCommentById([FromRoute] Guid id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        return Ok(comment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CommentResponse>> PublishComment([FromBody] CommentRequest commentDto)
    {
        var comment = await _commentService.PublishCommentAsync(commentDto);
        return Ok(comment);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> EditComment(
        [FromRoute] Guid id, [FromBody] CommentContentRequest commentDto)
    {
        await _commentService.EditCommentAsync(id, commentDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteComment([FromRoute] Guid id)
    {
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
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
    }
}
