using Posts.BusinessLogic.DTO.Requests;
using Posts.BusinessLogic.DTO.Responses;
using Posts.DataAccess.Extensions;

namespace Posts.BusinessLogic.Services.Contracts;

public interface IRatingService
{
    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no rating with given <paramref name="postId"/>
    /// and <paramref name="userId"/> is found.
    /// </summary>
    Task<RatingResponse> GetRatingOfPostByUserAsync(Guid postId, Guid userId);

    Task<RatingResponse> CreateRatingAsync(RatingRequest ratingDto);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no rating with given <paramref name="id"/> is found.
    /// </summary>
    Task EditRatingAsync(Guid id, RatingUpdateRequest ratingDto);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no rating with given <paramref name="id"/> is found.
    /// </summary>
    Task DeleteRatingAsync(Guid id);
}
