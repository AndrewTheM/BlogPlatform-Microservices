using BlogPlatform.Posts.DataAccess.Filters;
using System;

namespace BlogPlatform.Posts.BusinessLogic.Services.Contracts
{
    public interface IUriService
    {
        Uri GetPostsPageUri(PostFilter filter = null);
    }
}
