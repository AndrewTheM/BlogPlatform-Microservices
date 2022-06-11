using BlogPlatform.Shared.Common.Exceptions;
using Posts.DataAccess.Entities;

namespace Posts.DataAccess.Repositories.Contracts;

public interface IRatingRepository : IRepository<Rating>
{
    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no rating with given <paramref name="postId"/>
    /// and <paramref name="userId"/> is found.
    /// </summary>
    Task<Rating> GetRatingOfPostByUserAsync(Guid postId, Guid userId);
}
