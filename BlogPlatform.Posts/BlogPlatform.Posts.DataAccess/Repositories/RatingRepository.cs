using BlogPlatform.Posts.DataAccess.Context;
using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.DataAccess.Repositories
{
    public class RatingRepository : EntityRepository<Rating, int>, IRatingRepository
    {
        public RatingRepository(BlogContext context)
            : base(context)
        {
        }

        // TODO: work with other microservices
        public async Task<Rating> GetRatingOfPostByUserAsync(int postId, string userId)
        {
            return await EnsureEntityResultAsync(() =>
            {
                return _set//.Include(r => r.User)
                           .SingleAsync(r => r.PostId == postId && r.UserId == userId);
            });
        }
    }
}
