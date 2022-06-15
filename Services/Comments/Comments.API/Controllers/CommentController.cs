using BlogPlatform.Shared.Common.Filters;
using BlogPlatform.Shared.Common.Pagination;
using Comments.BusinessLogic.DTO.Requests;
using Comments.BusinessLogic.DTO.Responses;
using Comments.BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comments.API.Controllers;

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

    [HttpGet("post/{postId}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Page<CommentResponse>>> GetPageOfCommentsForPost(
        [FromRoute] Guid postId, [FromQuery] CommentFilter filter)
    {
        var page = await _commentService.GetPageOfCommentsForPostAsync(postId, filter);
        return Ok(page);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentResponse>> GetCommentById([FromRoute] Guid id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        return Ok(comment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CommentResponse>> PublishComment([FromBody] CommentRequest commentDto)
    {
        string userId = HttpContext.User.FindFirst("sub").Value;
        var comment = await _commentService.PublishCommentAsync(commentDto, Guid.Parse(userId));
        return Ok(comment);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> EditComment(
        [FromRoute] Guid id, [FromBody] CommentContentRequest commentDto)
    {
        bool userIsPermitted = await CheckIsAuthorOfCommentOrAdmin(id);
        if (!userIsPermitted)
        {
            return Forbid();
        }

        await _commentService.EditCommentAsync(id, commentDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteComment([FromRoute] Guid id)
    {
        bool userIsPermitted = await CheckIsAuthorOfCommentOrAdmin(id);
        if (!userIsPermitted)
        {
            return Forbid();
        }

        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    private async Task<bool> CheckIsAuthorOfCommentOrAdmin(Guid id)
    {
        string userId = HttpContext.User.FindFirst("sub").Value;
        return await _commentService.CheckIsCommentAuthorAsync(id, Guid.Parse(userId))
                || HttpContext.User.IsInRole("Admin");
    }
}
