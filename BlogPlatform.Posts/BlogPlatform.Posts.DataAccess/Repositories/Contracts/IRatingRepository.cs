using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Extensions;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.DataAccess.Repositories.Contracts
{
    public interface IRatingRepository<TId> : IRepository<Rating, TId>
    {
        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no rating with given <paramref name="postId"/>
        /// and <paramref name="userId"/> is found.
        /// </summary>
        Task<Rating> GetRatingOfPostByUserAsync(int postId, string userId);
    }

    public interface IRatingRepository : IRatingRepository<int>
    {
    }
}
