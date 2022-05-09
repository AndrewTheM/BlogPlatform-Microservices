using BlogPlatform.Comments.DataAccess.Entities;
using BlogPlatform.Comments.DataAccess.Filters;

namespace BlogPlatform.Comments.DataAccess.Repositories.Contracts;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IQueryable<Comment>> GetCommentsOfPostAsync(Guid postId);

    Task<IQueryable<Comment>> GetFilteredCommentsOfPostAsync(Guid postId, CommentFilter filter);

    Task<Comment> GetCommentWithAuthorAsync(Guid id);
}
