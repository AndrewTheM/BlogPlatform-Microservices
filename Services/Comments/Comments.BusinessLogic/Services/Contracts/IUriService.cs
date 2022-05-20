using Comments.DataAccess.Filters;

namespace Comments.BusinessLogic.Services.Contracts;

public interface IUriService
{
    Uri GetCommentsPageUri(Guid postId, CommentFilter filter = null);
}
