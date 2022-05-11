using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using BlogPlatform.Posts.BusinessLogic.DTO.Responses;
using BlogPlatform.Posts.BusinessLogic.Services.Contracts;
using BlogPlatform.Posts.DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Posts.API.Controllers;

[Route("api/ratings")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet("{postId}/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RatingResponse>> GetRatingByUserAndPost(
        [FromRoute] Guid postId, [FromRoute] Guid userId)
    {
        try
        {
            return await _ratingService.GetRatingOfPostByUserAsync(postId, userId);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RatingResponse>> CreateRating([FromBody] RatingRequest ratingDto)
    {
        return await _ratingService.CreateRatingAsync(ratingDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateRating(
        [FromRoute] Guid id, [FromBody] RatingUpdateRequest ratingDto)
    {
        try
        {
            await _ratingService.EditRatingAsync(id, ratingDto);
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
    public async Task<ActionResult> DeleteRating([FromRoute] Guid id)
    {
        try
        {
            await _ratingService.DeleteRatingAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }
}
