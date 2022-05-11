using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using BlogPlatform.Posts.BusinessLogic.Services.Contracts;
using BlogPlatform.Posts.DataAccess.Filters;
using BlogPlatform.Posts.BusinessLogic.DTO.Responses;
using BlogPlatform.Posts.DataAccess.Extensions;
using BlogPlatform.Posts.BusinessLogic.Helpers;

namespace BlogPlatform.Posts.API.Controllers;

[Route("api/posts")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    // TODO: logging on exceptions

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Page<PostResponse>>> GetPosts([FromQuery] PostFilter filter)
    {
        return await _postService.GetPageOfPostsAsync(filter);
    }

    [HttpGet("trending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PostResponse>>> GetTrendingPosts([FromQuery] int top = 5)
    {
        if (top is < 1 or > 20)
        {
            return BadRequest();
        }

        var trendingPosts = await _postService.GetTrendingPostsAsync(top);
        return Ok(trendingPosts);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostResponse>> GetPostById([FromRoute] Guid id)
    {
        try
        {
            return await _postService.FindPostAsync(id);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    // TODO: work with other microservices
    [HttpGet("complete/{titleIdentifier}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompletePostResponse>> GetCompletePostById(
        [FromRoute] string titleIdentifier)
    {
        try
        {
            var response = await _postService.GetCompletePostAsync(titleIdentifier);
            //response.CommentPage = await _commentService.GetPageOfCommentsForPostAsync(response.Id, new());
            return response;
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PostResponse>> CreatePost([FromBody] PostRequest postDto)
    {
        return await _postService.PublishPostAsync(postDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdatePost(
        [FromRoute] Guid id, [FromBody] PostRequest postDto)
    {
        try
        {
            await _postService.EditPostAsync(id, postDto);
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
    public async Task<ActionResult> DeletePost([FromRoute] Guid id)
    {
        try
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("{id}/tags")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SetTagsOfPost(
        [FromRoute] Guid id, [FromBody] PostTagsRequest tagsRequest)
    {
        try
        {
            await _postService.SetTagsOfPostAsync(id, tagsRequest.Tags);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }
}
