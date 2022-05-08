using BlogPlatform.Comments.DataAccess.Entities;
using BlogPlatform.Comments.DataAccess.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Comments.DataAccess.Repositories.Contracts
{
    public interface ICommentRepository<TId> : IRepository<Comment, TId>
    {
        Task<IQueryable<Comment>> GetCommentsOfPostAsync(TId id);

        Task<IQueryable<Comment>> GetFilteredCommentsOfPostAsync(int postId, CommentFilter filter);

        Task<Comment> GetCommentWithAuthorAsync(int id);
    }

    public interface ICommentRepository : ICommentRepository<int>
    {
    }
}
