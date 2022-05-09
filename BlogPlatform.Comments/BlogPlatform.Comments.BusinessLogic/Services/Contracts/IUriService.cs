using BlogPlatform.Comments.DataAccess.Filters;

namespace BlogPlatform.Comments.BusinessLogic.Services.Contracts;

public interface IUriService
{
    Uri GetCommentsPageUri(Guid postId, CommentFilter filter = null);
}
