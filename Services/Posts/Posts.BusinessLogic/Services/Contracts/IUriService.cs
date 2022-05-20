using Posts.DataAccess.Filters;

namespace Posts.BusinessLogic.Services.Contracts;

public interface IUriService
{
    Uri GetPostsPageUri(PostFilter filter = null);
}
