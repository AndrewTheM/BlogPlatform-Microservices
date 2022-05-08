using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using BlogPlatform.Posts.BusinessLogic.DTO.Responses;
using BlogPlatform.Posts.DataAccess.Extensions;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.BusinessLogic.Services.Contracts
{
    public interface IRatingService<TId>
    {
        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no rating with given <paramref name="postId"/>
        /// and <paramref name="userId"/> is found.
        /// </summary>
        Task<RatingResponse> GetRatingOfPostByUserAsync(int postId, string userId);

        Task<RatingResponse> CreateRatingAsync(RatingRequest ratingDto, string userId);

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no rating with given <paramref name="id"/> is found.
        /// </summary>
        Task EditRatingAsync(TId id, RatingRequest ratingDto);

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no rating with given <paramref name="id"/> is found.
        /// </summary>
        Task DeleteRatingAsync(TId id);
    }

    public interface IRatingService : IRatingService<int>
    {
    }
}
