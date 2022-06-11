using Aggregator.DTO;
using BlogPlatform.Shared.Common.Pagination;

namespace Aggregator.Services.Contracts;

public interface ICommentService
{
    Task<Page<CommentDto>> GetPageOfPostCommentsAsync(Guid postId);
}
