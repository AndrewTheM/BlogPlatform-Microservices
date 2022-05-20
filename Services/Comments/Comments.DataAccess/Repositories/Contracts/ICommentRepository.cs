using Comments.DataAccess.Entities;
using Comments.DataAccess.Filters;

namespace Comments.DataAccess.Repositories.Contracts;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetFilteredCommentsOfPostAsync(Guid postId, CommentFilter filter);

    Task<Comment> GetCommentWithAuthorAsync(Guid id);
}
