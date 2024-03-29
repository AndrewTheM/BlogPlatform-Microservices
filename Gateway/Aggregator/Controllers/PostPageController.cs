﻿using Aggregator.DTO;
using Aggregator.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    [Route("api/postpage")]
    [ApiController]
    [AllowAnonymous]
    public class PostPageController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostPageController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet("{titleIdentifier}")]
        public async Task<ActionResult<CompletePostDto>> GetPostPage(
            [FromRoute] string titleIdentifier)
        {
            var post = await _postService.GetCompletePostAsync(titleIdentifier);

            try
            {
                post.CommentPage = await _commentService.GetPageOfPostCommentsAsync(post.Id);
            }
            catch { }

            return post;
        }
    }
}
