using BlogPlatform.Comments.DataAccess.Filters;
using System;

namespace BlogPlatform.Comments.BusinessLogic.Services.Contracts
{
    public interface IUriService
    {
        Uri GetCommentsPageUri(int postId, CommentFilter filter = null);
    }
}
