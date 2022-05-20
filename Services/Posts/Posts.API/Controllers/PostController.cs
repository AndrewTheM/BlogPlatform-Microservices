using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using Posts.BusinessLogic.Helpers;
using Posts.DataAccess.Filters;
using Posts.BusinessLogic.Services.Contracts;
using Posts.BusinessLogic.DTO.Requests;
using Posts.BusinessLogic.DTO.Responses;

namespace Posts.API.Controllers;

[Route("api/posts")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IDistributedCache _cache;

    public PostController(IPostService postService, IDistributedCache cache)
    {
        _postService = postService;
        _cache = cache;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Page<PostResponse>>> GetPosts([FromQuery] PostFilter filter)
    {
        string cacheKey = $"posts_{filter.GetHashCode()}";
        var posts = await GetFromCache<Page<PostResponse>>(cacheKey);

        if (posts is null)
        {
            posts = await _postService.GetPageOfPostsAsync(filter);
            await SaveToCache(cacheKey, posts, options: new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(30),
            });
        }

        return Ok(posts);
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

        string cacheKey = $"trending_{top}";
        var posts = await GetFromCache<IEnumerable<PostResponse>>(cacheKey);

        if (posts is null)
        {
            posts = await _postService.GetTrendingPostsAsync(top);
            await SaveToCache(cacheKey, posts, options: new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2),
            });
        }

        return Ok(posts);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostResponse>> GetPostById([FromRoute] Guid id)
    {
        string cacheKey = $"post_{id}";
        var post = await GetFromCache<PostResponse>(cacheKey);

        if (post is null)
        {
            post = await _postService.FindPostAsync(id);
            await SaveToCache(cacheKey, post, options: new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(30),
            });
        }

        return Ok(post);
    }

    [HttpGet("complete/{titleIdentifier}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompletePostResponse>> GetCompletePostById(
        [FromRoute] string titleIdentifier)
    {
        var post = await GetFromCache<CompletePostResponse>(titleIdentifier);

        if (post is null)
        {
            post = await _postService.GetCompletePostAsync(titleIdentifier);
            await SaveToCache(titleIdentifier, post, options: new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(30),
            });
        }

        return Ok(post);
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
        await _postService.EditPostAsync(id, postDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePost([FromRoute] Guid id)
    {
        await _postService.DeletePostAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/tags")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SetTagsOfPost(
        [FromRoute] Guid id, [FromBody] PostTagsRequest tagsRequest)
    {
        await _postService.SetTagsOfPostAsync(id, tagsRequest.Tags);
        return NoContent();
    }

    private async Task<T> GetFromCache<T>(string key) where T : class
    {
        var cacheRecord = await _cache.GetAsync(key);
        if (cacheRecord is null)
        {
            return null;
        }

        var cacheJson = Encoding.UTF8.GetString(cacheRecord);
        return JsonSerializer.Deserialize<T>(cacheJson);
    }

    private async Task SaveToCache<T>(string key, T value, DistributedCacheEntryOptions options)
        where T : class
    {
        var valueJson = JsonSerializer.Serialize(value);
        var valueBytes = Encoding.UTF8.GetBytes(valueJson);
        await _cache.SetAsync(key, valueBytes, options);
    }
}
