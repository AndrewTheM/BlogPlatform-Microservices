using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Posts.BusinessLogic.DTO.Requests;
using Posts.BusinessLogic.DTO.Responses;
using Posts.BusinessLogic.Services.Contracts;
using System.Security.Claims;

namespace Posts.API.Controllers;

[Route("api/posts/ratings")]
[ApiController]
[Authorize]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet("{postId}/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RatingResponse>> GetRatingByUserAndPost(
        [FromRoute] Guid postId, [FromRoute] Guid userId)
    {
        return await _ratingService.GetRatingOfPostByUserAsync(postId, userId);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RatingResponse>> CreateRating([FromBody] RatingRequest ratingDto)
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _ratingService.CreateRatingAsync(ratingDto, Guid.Parse(userId));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateRating(
        [FromRoute] Guid id, [FromBody] RatingUpdateRequest ratingDto)
    {
        await _ratingService.EditRatingAsync(id, ratingDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRating([FromRoute] Guid id)
    {
        await _ratingService.DeleteRatingAsync(id);
        return NoContent();
    }
}
