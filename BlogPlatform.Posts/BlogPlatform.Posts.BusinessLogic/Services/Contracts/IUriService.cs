using BlogPlatform.Posts.DataAccess.Filters;

namespace BlogPlatform.Posts.BusinessLogic.Services.Contracts;

public interface IUriService
{
    Uri GetPostsPageUri(PostFilter filter = null);
}
