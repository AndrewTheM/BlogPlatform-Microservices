using BlogPlatform.Comments.DataAccess.Context;
using BlogPlatform.Comments.DataAccess.Entities;
using BlogPlatform.Comments.DataAccess.Extensions;
using BlogPlatform.Comments.DataAccess.Filters;
using BlogPlatform.Comments.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Comments.DataAccess.Repositories
{
    public class CommentRepository : EntityRepository<Comment, int>, ICommentRepository
    {
        public CommentRepository(BlogContext context)
            : base(context)
        {
        }

        // TODO: work with other microservices
        public async Task<IQueryable<Comment>> GetCommentsOfPostAsync(int postId)
        {
            //var post = await _context.Set<Post>().FindAsync(postId);

            //if (post is null)
            //    throw new EntityNotFoundException();

            var comments = _set//.Include(c => c.Author)
                               .Where(c => c.PostId == postId)
                               .OrderByDescending(c => c.CreatedOn);
            return await Task.FromResult(comments);
        }

        public async Task<IQueryable<Comment>> GetFilteredCommentsOfPostAsync(int postId, CommentFilter filter)
        {
            var comments = await this.GetCommentsOfPostAsync(postId);

            if (filter is null)
                return comments;

            return comments.Where(c => filter.Content == null || c.Content.Contains(filter.Content));
        }

        // TODO: work with other microservices
        public async Task<Comment> GetCommentWithAuthorAsync(int id)
        {
            return await EnsureEntityResultAsync(() =>
            {
                return _set//.Include(c => c.Author)
                           .SingleAsync(c => c.Id == id);
            });
        }
    }
}
