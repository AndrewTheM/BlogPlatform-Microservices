using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Extensions;
using BlogPlatform.Posts.DataAccess.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.DataAccess.Repositories.Contracts
{
    public interface IPostRepository<TId> : IRepository<Post, TId>
    {
        Task<IQueryable<Post>> GetNewestPostsWithAuthorsAndTagsAsync();

        Task<IQueryable<Post>> GetFilteredPostsAsync(PostFilter filter);

        Task<IQueryable<Post>> GetTopRatedPostsWithAuthorsAsync(int count);

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no post with given <paramref name="id"/> is found.
        /// </summary>
        Task<Post> GetPostWithContentAsync(TId id);

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no post with given <paramref name="id"/> is found.
        /// </summary>
        Task<Post> GetPostWithTagsAsync(TId id);

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no post with given <paramref name="titleIdentifier"/> is found.
        /// </summary>
        Task<Post> GetCompletePostAsync(string titleIdentifier);

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no post with given <paramref name="id"/> is found.
        /// </summary>
        Task<double> CalculatePostRatingAsync(TId id);
    }

    public interface IPostRepository : IPostRepository<int>
    {
    }
}
