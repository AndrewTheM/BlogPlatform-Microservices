using BlogPlatform.Shared.Common.Filters;

namespace BlogPlatform.Shared.Services.Contracts;

public interface IUriService
{
    Uri GetPostsPageUri(PostFilter filter = null);

    Uri GetCommentsPageUri(Guid postId, CommentFilter filter = null);
}
