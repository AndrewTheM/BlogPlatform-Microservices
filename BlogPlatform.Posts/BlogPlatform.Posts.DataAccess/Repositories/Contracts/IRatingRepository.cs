using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Extensions;

namespace BlogPlatform.Posts.DataAccess.Repositories.Contracts;

public interface IRatingRepository : IRepository<Rating>
{
    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no rating with given <paramref name="postId"/>
    /// and <paramref name="userId"/> is found.
    /// </summary>
    Task<Rating> GetRatingOfPostByUserAsync(Guid postId, Guid userId);
}
