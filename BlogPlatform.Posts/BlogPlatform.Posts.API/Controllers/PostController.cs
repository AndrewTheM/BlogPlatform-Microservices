using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using BlogPlatform.Posts.BusinessLogic.Services.Contracts;
using BlogPlatform.Posts.DataAccess.Filters;
using BlogPlatform.Posts.BusinessLogic.DTO.Responses;
using BlogPlatform.Posts.DataAccess.Extensions;
using BlogPlatform.Posts.BusinessLogic.Helpers;

namespace BlogPlatform.Posts.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [Authorize(Roles = "Admin, Author")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // TODO: logging on exceptions

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<PostResponse>>> GetPosts([FromQuery] PostFilter filter)
        {
            return await _postService.GetPageOfPostsAsync(filter);
        }

        [HttpGet("trending")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetTrendingPosts([FromQuery] int top = 5)
        {
            if (top is < 1 or > 20)
                return BadRequest();

            var trendingPosts = await _postService.GetTrendingPostsAsync(top);
            return Ok(trendingPosts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostResponse>> GetPostById([FromRoute] int id)
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

        // TODO: reimagine this method
        //
        //[HttpGet("{id}/comments")]
        //[AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Page<CommentResponse>>> GetCommentsByPostId([FromRoute] int id,
        //                                                                           [FromQuery] CommentFilter filter)
        //{
        //    try
        //    {
        //        return await _commentService.GetPageOfCommentsForPostAsync(id, filter);
        //    }
        //    catch (EntityNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}

        // TODO: work with other microservices
        [HttpGet("complete/{titleIdentifier}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompletePostResponse>> GetCompletePostById([FromRoute] string titleIdentifier)
        {
            try
            {
                CompletePostResponse response = await _postService.GetCompletePostAsync(titleIdentifier);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PostResponse>> CreatePost([FromBody] PostRequest postDto)
        {
            string userId = HttpContext.User.FindFirst("id").Value;
            return await _postService.PublishPostAsync(postDto, userId);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePost([FromRoute] int id,
                                                   [FromBody] PostRequest postDto)
        {
            try
            {
                bool userIsPermitted = await CheckIsAuthorOfPostOrAdminAsync(id);

                if (!userIsPermitted)
                    return Forbid();

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePost([FromRoute] int id)
        {
            try
            {
                bool userIsPermitted = await CheckIsAuthorOfPostOrAdminAsync(id);

                if (!userIsPermitted)
                    return Forbid();

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SetTagsOfPost([FromRoute] int id,
                                                      [FromBody] PostTagsRequest tagsRequest)
        {
            try
            {
                bool userIsPermitted = await CheckIsAuthorOfPostOrAdminAsync(id);

                if (!userIsPermitted)
                    return Forbid();

                await _postService.SetTagsOfPostAsync(id, tagsRequest.Tags);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        private async Task<bool> CheckIsAuthorOfPostOrAdminAsync(int id)
        {
            string userId = HttpContext.User.FindFirst("id").Value;
            return await _postService.CheckIsPostAuthorAsync(id, userId)
                   || HttpContext.User.IsInRole("Admin");
        }
    }
}
