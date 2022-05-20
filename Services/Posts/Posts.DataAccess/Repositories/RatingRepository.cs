using Microsoft.EntityFrameworkCore;
using Posts.DataAccess.Context;
using Posts.DataAccess.Entities;
using Posts.DataAccess.Repositories.Contracts;

namespace Posts.DataAccess.Repositories;

public class RatingRepository : EntityRepository<Rating>, IRatingRepository
{
    public RatingRepository(BlogContext context)
        : base(context)
    {
    }

    // TODO: work with other microservices
    public async Task<Rating> GetRatingOfPostByUserAsync(Guid postId, Guid userId)
    {
        return await EnsureEntityResultAsync(() =>
        {
            return _set//.Include(r => r.User)
                .SingleAsync(r => r.PostId == postId && r.UserId == userId);
        });
    }
}
